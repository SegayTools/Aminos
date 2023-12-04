using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;
using Aminos.Services.StatisticLoggers.Game;
using Aminos.Services.StatisticLoggers.General;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.General;

[RegisterInjectable(typeof(GeneralHandler))]
public class GeneralHandler
{
    private readonly AminosDB aminosDB;
    private readonly ILogger<GeneralHandler> logger;
    private readonly IGeneralStatisticLogger generalStatisticLogger;
    private readonly IGameStatisticLogger gameStatisticLogger;

    public GeneralHandler(
        AminosDB aminosDB,
        ILogger<GeneralHandler> logger,
        IGeneralStatisticLogger generalStatisticLogger,
        IGameStatisticLogger gameStatisticLogger)
    {
        this.aminosDB = aminosDB;
        this.logger = logger;
        this.generalStatisticLogger = generalStatisticLogger;
        this.gameStatisticLogger = gameStatisticLogger;
    }

    public async ValueTask<CommonApiResponse> GetAnnouncements(int takeCount, int skipCount)
    {
        var arr = await aminosDB.Announcements.Skip(skipCount).Take(takeCount).ToArrayAsync();
        return new CommonApiResponse<Announcement[]>(true, arr);
    }

    public async ValueTask<CommonApiResponse> AddAnnouncement(UserAccount user, Announcement announcement)
    {
        if (user is null)
            return new CommonApiResponse(false, "无法获取用户信息");

        await using var transaction = await aminosDB.Database.BeginTransactionAsync();
        {
            announcement.UserAccount = user;
            await aminosDB.Announcements.AddAsync(announcement);
            await aminosDB.SaveChangesAsync();
        }
        await transaction.CommitAsync();

        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> DeleteAnnouncement(UserAccount user, int announcementId)
    {
        if (user is null)
            return new CommonApiResponse(false, "无法获取用户信息");

        await using var transaction = await aminosDB.Database.BeginTransactionAsync();
        {
            var announcement = await aminosDB.Announcements.FirstOrDefaultAsync(x => x.Id == announcementId);
            if (announcement is not null)
                aminosDB.Announcements.Remove(announcement);
            else
                return new CommonApiResponse(false, "公告不存在");
            await aminosDB.SaveChangesAsync();
        }
        await transaction.CommitAsync();

        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> GetGeneralStatistic()
    {
        var items = await generalStatisticLogger.DumpStatistics();
        return new CommonApiResponse<StatisticItem[]>(true, items.ToArray());
    }

    public async ValueTask<CommonApiResponse> GetGameStatistic()
    {
        var items = await gameStatisticLogger.DumpStatistics();
        return new CommonApiResponse<StatisticItem[]>(true, items.ToArray());
    }
}