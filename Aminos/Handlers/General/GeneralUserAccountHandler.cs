using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.Title.SDEZ;
using Aminos.Models.General;
using Aminos.Models.General.Tables;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		private readonly MD5 md5;

		public GeneralUserAccountHandler(AminosDB aminosDB, ILogger<GeneralUserAccountHandler> logger)
		{
			this.aminosDB = aminosDB;
			this.logger = logger;
			md5 = MD5.Create();
		}

		public async ValueTask<CommonApiResponse> Login(string userName, string passwordHash, HttpContext context)
		{
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

				var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
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

		public async ValueTask<CommonApiResponse> Register(string userName, string passwordHash, string email)
		{
			if (await aminosDB.UserAccounts.AnyAsync(x => x.Name == userName))
				return new CommonApiResponse(false, "用户名已被使用");
			if (await aminosDB.UserAccounts.AnyAsync(x => x.Email == email))
				return new CommonApiResponse(false, "邮箱已被登记");

			var newUser = new Models.General.Tables.UserAccount()
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
				return new CommonApiResponse(true);
			}
			catch (Exception e)
			{
				var trackId = logger.LogErrorAndGetTrackId(e, $"数据库添加用户失败:{JsonSerializer.Serialize(newUser)}");
				return new CommonApiInternalExceptionResponse(trackId);
			}
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
	}
}
