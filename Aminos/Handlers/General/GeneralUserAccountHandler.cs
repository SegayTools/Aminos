using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Aminos.Authorization;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;
using Aminos.Services.Emails;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.General;

[RegisterInjectable(typeof(GeneralUserAccountHandler))]
public class GeneralUserAccountHandler
{
    private static readonly Dictionary<string, ResetPasswordToken> hodingTokens = new();
    private static readonly MD5 md5 = MD5.Create();
    private static readonly SHA512 sha512 = SHA512.Create();

    private readonly AminosDB aminosDB;
    private readonly IDataProtector dataProtection;
    private readonly ILogger<GeneralUserAccountHandler> logger;

    public GeneralUserAccountHandler(AminosDB aminosDB, ILogger<GeneralUserAccountHandler> logger,
        IEmailSender emailSender, IDataProtectionProvider dataProtection)
    {
        this.aminosDB = aminosDB;
        this.logger = logger;
        this.dataProtection = dataProtection.CreateProtector("token");
    }

    public async ValueTask<CommonApiResponse> Login(string userName, string passwordHash, HttpContext context)
    {
        passwordHash = RehashPasswordHash(passwordHash);

        var user = await aminosDB.UserAccounts.FirstOrDefaultAsync(x =>
            x.Name == userName && passwordHash == x.PasswordHash);
        
        if (user is not null)
        {
            var hash = Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(user + passwordHash)));

            var roleString = user.Role switch
            {
                AuthRolePolicy.User => AuthRolePolicyString.UserRole,
                AuthRolePolicy.Admin => AuthRolePolicyString.AdminRole,
                AuthRolePolicy.Owner => AuthRolePolicyString.OwnerRole,
                _ => string.Empty
            };
            
            var claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                //new Claim(ClaimTypes.Name, userName),
                //new Claim(ClaimTypes.Hash, hash),
                new(ClaimTypes.Role, roleString)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var userPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true
            };

            try
            {
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                    authProperties);
                
                //update LastLoginWebDate
                user.LastLoginWebDate = DateTime.Now;
                await aminosDB.SaveChangesAsync();
                
                return new CommonApiResponse<UserAccount>(true, user);
            }
            catch (Exception e)
            {
                var trackId = logger.LogErrorAndGetTrackId(e, $"调用SignInAsync()失败:{JsonSerializer.Serialize(user)}");
                return new CommonApiInternalExceptionResponse(trackId);
            }
        }

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

    public async ValueTask<CommonApiResponse> UpdatePassword(UserAccount curUser, string newPasswordHash,
        string tokenHandle)
    {
        if (curUser is null)
            if (!string.IsNullOrWhiteSpace(tokenHandle))
            {
                //get user from token
                if (hodingTokens.TryGetValue(tokenHandle, out var token))
                    if (DateTime.Now < token.Expired)
                    {
                        var userId = token.UserId;
                        curUser = await aminosDB.UserAccounts.FindAsync(userId);
                        logger.LogDebug($"get user {userId} from token {tokenHandle}");
                    }

                hodingTokens.Remove(tokenHandle);
            }

        if (curUser is null)
            return new CommonApiResponse(false, "用户未登录或Token失效");

        newPasswordHash = RehashPasswordHash(newPasswordHash);
        curUser.PasswordHash = newPasswordHash;
        await aminosDB.SaveChangesAsync();
        return new CommonApiResponse(true);
    }

    internal async ValueTask<CommonApiResponse> SendToken(string email)
    {
        var user = await aminosDB.UserAccounts.FirstOrDefaultAsync(x => x.Email == email);
        if (user != null)
        {
            var tokenHandle = RandomHelper.RandomString(RandomHelper.LettersAndDigits, 16);
            var token = new ResetPasswordToken
            {
                Expired = DateTime.Now + TimeSpan.FromMinutes(10),
                UserId = user.Id
            };

            foreach (var item in hodingTokens.Where(x => x.Value.UserId == user.Id).ToArray())
                hodingTokens.Remove(item.Key);
            hodingTokens[tokenHandle] = token;
            logger.LogDebug($"save user {user.Id} from token {tokenHandle}");
        }

        //固定true，避免被恶意利用检查邮箱是否被使用
        return new CommonApiResponse(true);
    }

    public ValueTask<CommonApiResponse> GetActivities(UserAccount curUser, int skipCount, int takeCount)
    {
        if (curUser is null)
            return ValueTask.FromResult(new CommonApiResponse(false, "用户未登录"));

        var arr = curUser.Activities.Skip(skipCount).Take(takeCount).ToArray();
        return ValueTask.FromResult<CommonApiResponse>(new CommonApiResponse<Activity[]>(true, arr));
    }

    public async ValueTask<CommonApiResponse> ChangeUserRole(UserAccount curUser, Guid targetUserGuid,
        AuthRolePolicy newRole)
    {
        if (curUser is null)
            return new CommonApiResponse(false, "用户未登录");
        if (await aminosDB.UserAccounts.FindAsync(targetUserGuid) is not UserAccount target)
            return new CommonApiResponse(false, "找不到目标用户");
        if (curUser.Role != AuthRolePolicy.Admin)
            return new CommonApiResponse(false, "当前用户没权执行");

        var old = target.Role;
        target.Role = newRole;
        await aminosDB.SaveChangesAsync();

        logger.LogInformation(
            $"User {curUser.Name}(id:{curUser.Id}) has modified {target.Name}(id:{target.Id})'s role from {old} to {newRole}");
        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> Register(string userName, string passwordHash, string email)
    {
        passwordHash = RehashPasswordHash(passwordHash);

        if (await aminosDB.UserAccounts.AnyAsync(x => x.Name == userName))
            return new CommonApiResponse(false, "用户名已被使用");
        if (await aminosDB.UserAccounts.AnyAsync(x => x.Email == email))
            return new CommonApiResponse(false, "邮箱已被登记");

        var newUser = new UserAccount
        {
            RegisterDate = DateTime.Now,
            Email = email,
            Role = AuthRolePolicy.User,
            Name = userName,
            PasswordHash = passwordHash
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
            var trackId = logger.LogErrorAndGetTrackId(e, "调用SignOutAsync()失败");
            return new CommonApiInternalExceptionResponse(trackId);
        }
    }

    private string RehashPasswordHash(string passwordHash)
    {
        return Convert.ToHexString(sha512.ComputeHash(md5.ComputeHash(Encoding.UTF8.GetBytes(passwordHash))));
    }

    private class ResetPasswordToken
    {
        public DateTime Expired { get; set; }
        public Guid UserId { get; set; }
    }

    public async ValueTask<CommonApiResponse> GetGameplayLogs(UserAccount curUser, DateTime fromDateTime, DateTime toDateTime)
    {
        if (curUser is null)
            return new CommonApiResponse(false, "用户未登录");

        return new CommonApiResponse<GameplayLog[]>(true,
            curUser.GameplayLogs.Where(x => x.Time >= fromDateTime && x.Time <= toDateTime).ToArray());
    }
}