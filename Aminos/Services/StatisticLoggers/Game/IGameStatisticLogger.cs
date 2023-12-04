namespace Aminos.Services.StatisticLoggers.Game;

public interface IGameStatisticLogger : IStatisticLogger
{
    public const int LogType_SDEZ_UserOnlineCount = 0;
    
    ValueTask AggregateValue(int type, int offsetValue);
    ValueTask SetValue(int type, int newValue);
}