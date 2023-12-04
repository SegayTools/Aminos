using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Aminos.Services.StatisticLoggers.Game;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserLoginHandler))]
public class MaimaiDXUserLoginHandler
{
    private readonly IGameStatisticLogger gameStatisticLogger;
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserLoginHandler(MaimaiDXDB maimaiDxDB, IGameStatisticLogger gameStatisticLogger)
    {
        this.maimaiDxDB = maimaiDxDB;
        this.gameStatisticLogger = gameStatisticLogger;
    }

    public async ValueTask<UserLoginResponseVO> UserLogin(UserLoginRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var response = new UserLoginResponseVO
        {
            returnCode = 1,
            lastLoginDate = userDetail?.lastLoginDate ?? default,
            Bearer = string.Empty,
            consecutiveLoginCount = 0,
            loginCount = 1,
            loginId = 1
        };

        await gameStatisticLogger.AggregateValue(IGameStatisticLogger.LogType_SDEZ_UserOnlineCount, 1);
        return response;
    }

    public async ValueTask<UserLogoutResponseVO> UserLogout(UserLogoutRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var response = new UserLogoutResponseVO
        {
            returnCode = 1
        };

        await gameStatisticLogger.AggregateValue(IGameStatisticLogger.LogType_SDEZ_UserOnlineCount, -1);
        return response;
    }
}