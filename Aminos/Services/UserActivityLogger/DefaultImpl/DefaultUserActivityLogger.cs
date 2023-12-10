using Aminos.Core.Models.General.Emuns;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;

namespace Aminos.Services.UserActivityLogger.DefaultImpl;

[RegisterInjectable(typeof(IUserActivityLogger))]
public class DefaultUserActivityLogger : IUserActivityLogger
{
    private readonly AminosDB aminosDB;

    public DefaultUserActivityLogger(AminosDB aminosDB)
    {
        this.aminosDB = aminosDB;
    }

    public async ValueTask LogActivity(UserAccount user, ActivityType activityType, string message)
    {
        using var disp = await aminosDB.Database.BeginTransactionAsync();
        user.Activities.Add(new Activity
        {
            Content = message,
            Time = DateTime.Now,
            Type = activityType
        });
        await aminosDB.SaveChangesAsync();
        await disp.CommitAsync();
    }

    public async ValueTask LogGameplay(UserAccount user, string gameId)
    {
        using var disp = await aminosDB.Database.BeginTransactionAsync();
        user.GameplayLogs.Add(new GameplayLog
        {
            Time = DateTime.Now,
            GameId = gameId
        });
        await aminosDB.SaveChangesAsync();
        await disp.CommitAsync();
    }
}