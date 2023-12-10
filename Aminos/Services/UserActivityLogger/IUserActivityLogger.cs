using Aminos.Core.Models.General.Emuns;
using Aminos.Core.Models.General.Tables;

namespace Aminos.Services.UserActivityLogger;

public interface IUserActivityLogger
{
    /// <summary>
    /// 记录玩家活动
    /// </summary>
    /// <param name="user"></param>
    /// <param name="activityType"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public ValueTask LogActivity(UserAccount user, ActivityType activityType, string message);
    
    /// <summary>
    /// 记录玩家一次游戏的记录
    /// </summary>
    /// <param name="user"></param>
    /// <param name="gameId">SDEZ/SDDT之类</param>
    /// <returns></returns>
    public ValueTask LogGameplay(UserAccount user, string gameId);
}