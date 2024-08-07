﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;

namespace AminosUI.Services.Applications;

public interface IUserManager
{
    UserAccount CurrentUser { get; }

    ValueTask<CommonApiResponse> Login(string username, string password, CancellationToken cancellation);
    ValueTask<CommonApiResponse> Logout(CancellationToken cancellation);

    ValueTask<CommonApiResponse> Register(string userName, string password, string email,
        CancellationToken cancellation);

    ValueTask<CommonApiResponse> ResetPassword(string newPassword, CancellationToken cancellation);
    ValueTask<CommonApiResponse> SendToken(string email, CancellationToken cancellation);
    ValueTask<CommonApiResponse> LoginByCookies(CookieContainer container, CancellationToken cancellation);
}