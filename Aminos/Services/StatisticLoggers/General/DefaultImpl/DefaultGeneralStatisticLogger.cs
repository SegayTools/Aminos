using System.Diagnostics;
using Aminos.Core.Models.General;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Services.StatisticLoggers.General.DefaultImpl;

[RegisterInjectable(typeof(IGeneralStatisticLogger))]
public class DefaultGeneralStatisticLogger : IGeneralStatisticLogger
{
    private readonly AminosDB aminosDb;
    private static int cachedRecentUserValue;
    private static DateTime cachedTime = DateTime.MinValue;
    private static int cachedTotalUserValue;
    private static int uiVisitCount;

    public DefaultGeneralStatisticLogger(AminosDB aminosDB)
    {
        aminosDb = aminosDB;
    }

    public ValueTask AddUIVisitCount()
    {
        Interlocked.Add(ref uiVisitCount, 1);
        return ValueTask.CompletedTask;
    }

    public async ValueTask<IEnumerable<StatisticItem>> DumpStatistics()
    {
        var items = new StatisticItem[]
        {
            new()
            {
                Name = "服务器运行时长",
                Description = "数值以毫秒为单位",
                ValueType = IGeneralStatisticLogger.LogType_ServerRunning,
                Value = await GetServerRunning()
            },
            new()
            {
                Name = "客户端被访问次数",
                Description = "仅限于本次服务器运行内统计",
                ValueType = IGeneralStatisticLogger.LogType_UIVisited,
                Value = uiVisitCount
            },
            new()
            {
                Name = "已注册用户数量",
                Description = string.Empty,
                ValueType = IGeneralStatisticLogger.LogType_UserRegistered,
                Value = await GetUserRegistered()
            },
            new()
            {
                Name = "近期活跃用户数量",
                Description = "一周内游戏或网页访问",
                ValueType = IGeneralStatisticLogger.LogType_UserRecentActived,
                Value = await GetUserRecentActived()
            }
        };
        return items;
    }

    private async ValueTask<float> GetUserRecentActived()
    {
        await CheckAndUpdateCached();
        return cachedRecentUserValue;
    }

    private async ValueTask CheckAndUpdateCached()
    {
        var nowTime = DateTime.Now;
        if (cachedTime + TimeSpan.FromMinutes(5) > nowTime)
            return;
        //一周内算活跃~
        var timeSpan = TimeSpan.FromDays(7);
        cachedRecentUserValue = (await aminosDb.UserAccounts.ToArrayAsync())
            .Where(x => x.LastPlayDate + timeSpan > nowTime || x.LastLoginWebDate + timeSpan > nowTime)
            .Count();
        cachedTotalUserValue = await aminosDb.UserAccounts.CountAsync();
        cachedTime = nowTime;
    }

    private async ValueTask<float> GetUserRegistered()
    {
        await CheckAndUpdateCached();
        return cachedTotalUserValue;
    }

    private ValueTask<float> GetServerRunning()
    {
        return ValueTask.FromResult((float) (Process.GetCurrentProcess().StartTime - DateTime.Now).TotalMilliseconds);
    }
}