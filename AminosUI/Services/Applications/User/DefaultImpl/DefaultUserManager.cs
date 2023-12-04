using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils.MethodExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace AminosUI.Services.Applications.User.DefaultImpl;

[RegisterInjectable(typeof(IUserManager), ServiceLifetime.Singleton)]
internal class DefaultUserManager : IUserManager
{
    private readonly IApplicationHttpFactory applicationHttpFactory;
    private readonly MD5 md5;

    public DefaultUserManager(IApplicationHttpFactory applicationHttpFactory)
    {
        this.applicationHttpFactory = applicationHttpFactory;
        md5 = MD5.Create();
    }

    public UserAccount CurrentUser { get; private set; }

    public async ValueTask<CommonApiResponse> Login(string username, string password,
        CancellationToken cancellationToken)
    {
        var passwordHash = HashPassword(password);

        var resp = await applicationHttpFactory.PostAsCommonApi<UserAccount>("api/Account/Login", new
        {
            passwordHash,
            username
        }, cancellationToken);

        if (resp.isSuccess)
            CurrentUser = resp.obj;

        return resp;
    }

    public async ValueTask<CommonApiResponse> Logout(CancellationToken cancellationToken)
    {
        var resp = await applicationHttpFactory.PostAsCommonApi("api/Account/Logout", default,
            cancellationToken);

        if (resp.isSuccess)
        {
            CurrentUser = default;
            applicationHttpFactory.ResetAll();
        }

        return resp;
    }

    public async ValueTask<CommonApiResponse> Register(string userName, string password, string email,
        CancellationToken cancellation)
    {
        var passwordHash = HashPassword(password);

        var resp = await applicationHttpFactory.PostAsCommonApi("api/Account/Register", new
        {
            userName,
            passwordHash,
            email
        }, cancellation);

        if (resp.isSuccess)
        {
            CurrentUser = default;
            applicationHttpFactory.ResetAll();
        }

        return resp;
    }

    public async ValueTask<CommonApiResponse> ResetPassword(string newPassword, CancellationToken cancellation)
    {
        var passwordHash = HashPassword(newPassword);

        var resp = await applicationHttpFactory.PostAsCommonApi("api/Account/UpdatePassword", new
        {
            newPasswordHash = passwordHash
        }, cancellation);

        if (resp.isSuccess)
        {
            CurrentUser = default;
            applicationHttpFactory.ResetAll();
        }

        return resp;
    }

    public async ValueTask<CommonApiResponse> SendToken(string email, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    private string HashPassword(string password)
    {
        return Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(password + "2857")));
    }
}