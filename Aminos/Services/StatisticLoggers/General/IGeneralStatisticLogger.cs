namespace Aminos.Services.StatisticLoggers.General;

public interface IGeneralStatisticLogger : IStatisticLogger
{
    public const int LogType_UserRegistered = 0;
    public const int LogType_ServerRunning = 1;
    public const int LogType_UIVisited = 2;
    public const int LogType_UserRecentActived = 3;

    ValueTask AddUIVisitCount();
}