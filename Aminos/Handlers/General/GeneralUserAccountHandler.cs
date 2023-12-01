using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.Title.SDEZ;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Aminos.Handlers.General
{
	[RegisterInjectable(typeof(GeneralUserAccountHandler))]
	public class GeneralUserAccountHandler
	{
		private readonly AminosDB aminosDB;
		private readonly ILogger<GeneralUserAccountHandler> logger;
		private static readonly MD5 md5 = MD5.Create();
		private static readonly SHA512 sha512 = SHA512.Create();

		public GeneralUserAccountHandler(AminosDB aminosDB, ILogger<GeneralUserAccountHandler> logger)
		{
			this.aminosDB = aminosDB;
			this.logger = logger;
		}

		public async ValueTask<CommonApiResponse> Login(string userName, string passwordHash, HttpContext context)
		{
			passwordHash = RehashPasswordHash(passwordHash);

			var user = await aminosDB.UserAccounts.FirstOrDefaultAsync(x => x.Name == userName && passwordHash == x.PasswordHash);
			var hash = Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(user + passwordHash)));

			var roleString = user.Role switch
			{
				AuthRolePolicy.User => AuthRolePolicyString.UserRole,
				AuthRolePolicy.Admin => AuthRolePolicyString.AdminRole,
				AuthRolePolicy.Owner => AuthRolePolicyString.OwnerRole,
				_ => string.Empty
			};

			if (user is not null)
			{
				var claims = new List<Claim>() {
					new Claim("UserId", user.Id.ToString()),
					//new Claim(ClaimTypes.Name, userName),
					//new Claim(ClaimTypes.Hash, hash),
					new Claim(ClaimTypes.Role, roleString)
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var userPrincipal = new ClaimsPrincipal(identity);

				var authProperties = new AuthenticationProperties()
				{
					IsPersistent = true,
					AllowRefresh = true,
				};

				try
				{
					await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
					return new CommonApiResponse(true);
				}
				catch (Exception e)
				{
					var trackId = logger.LogErrorAndGetTrackId(e, $"调用SignInAsync()失败:{JsonSerializer.Serialize(user)}");
					return new CommonApiInternalExceptionResponse(trackId);
				}
			}
			else
				return new CommonApiResponse(false);
		}

		public async ValueTask<CommonApiResponse> Update(UserAccount curUser, string newEmail, string newName)
		{
			if (curUser is null)
				return new CommonApiResponse(false, "用户未登录");

			curUser.Email = newEmail;
			curUser.Name = newName;

			await aminosDB.SaveChangesAsync();
			return new CommonApiResponse(true);
		}

		public async ValueTask<CommonApiResponse> UpdatePassword(UserAccount curUser, string newPasswordHash)
		{
			if (curUser is null)
				return new CommonApiResponse(false, "用户未登录");

			newPasswordHash = RehashPasswordHash(newPasswordHash);
			curUser.PasswordHash = newPasswordHash;
			await aminosDB.SaveChangesAsync();
			return new CommonApiResponse(true);
		}

		public async ValueTask<CommonApiResponse> ChangeUserRole(UserAccount curUser, Guid targetUserGuid, AuthRolePolicy newRole)
		{
			if (curUser is null)
				return new CommonApiResponse(false, "用户未登录");
			if ((await aminosDB.UserAccounts.FindAsync(targetUserGuid)) is not UserAccount target)
				return new CommonApiResponse(false, "找不到目标用户");
			if (curUser.Role != AuthRolePolicy.Admin)
				return new CommonApiResponse(false, "当前用户没权执行");

			var old = target.Role;
			target.Role = newRole;
			await aminosDB.SaveChangesAsync();

			logger.LogInformation($"User {curUser.Name}(id:{curUser.Id}) has modified {target.Name}(id:{target.Id})'s role from {old} to {newRole}");
			return new CommonApiResponse(true);
		}

		public async ValueTask<CommonApiResponse> Register(string userName, string passwordHash, string email)
		{
			passwordHash = RehashPasswordHash(passwordHash);

			if (await aminosDB.UserAccounts.AnyAsync(x => x.Name == userName))
				return new CommonApiResponse(false, "用户名已被使用");
			if (await aminosDB.UserAccounts.AnyAsync(x => x.Email == email))
				return new CommonApiResponse(false, "邮箱已被登记");

			var newUser = new UserAccount()
			{
				RegisterDate = DateTime.Now,
				Email = email,
				Role = AuthRolePolicy.User,
				Name = userName,
				PasswordHash = passwordHash,
			};

			try
			{
				await aminosDB.AddAsync(newUser);
				await aminosDB.SaveChangesAsync();
				return new CommonApiResponse(true);
			}
			catch (Exception e)
			{
				var trackId = logger.LogErrorAndGetTrackId(e, $"数据库添加用户失败:{JsonSerializer.Serialize(newUser)}");
				return new CommonApiInternalExceptionResponse(trackId);
			}
		}

		public ValueTask<CommonApiResponse> Get(UserAccount user)
		{
			return ValueTask.FromResult<CommonApiResponse>(new CommonApiResponse<UserAccount>(true, user));
		}

		public async ValueTask<CommonApiResponse> Logout(HttpContext context)
		{
			try
			{
				await context.SignOutAsync();
				return new CommonApiResponse(true);
			}
			catch (Exception e)
			{
				var trackId = logger.LogErrorAndGetTrackId(e, $"调用SignOutAsync()失败");
				return new CommonApiInternalExceptionResponse(trackId);
			}
		}

		private string RehashPasswordHash(string passwordHash)
		{
			return Convert.ToHexString(sha512.ComputeHash(md5.ComputeHash(Encoding.UTF8.GetBytes(passwordHash))));
		}
	}
}
