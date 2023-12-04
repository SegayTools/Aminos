using Aminos.Core.Models.General;
using Aminos.Core.Services.Injections.Attrbutes;

namespace Aminos.Services.StatisticLoggers.Game.DefaultImpl;

[RegisterInjectable(typeof(IGameStatisticLogger), ServiceLifetime.Singleton)]
public class DefaultGameStatisticLoggerBase : CommonStatisticLoggerBase, IGameStatisticLogger
{
    protected override StatisticItem GenerateStatisticItem(int type, float value)
    {
        StatisticItem item = type switch
        {
            IGameStatisticLogger.LogType_SDEZ_UserOnlineCount => new StatisticItem
            {
                Name = "当前玩家在线",
                Description = string.Empty
            },
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "type不符合")
        };

        item.ValueType = type;
        item.Value = value;

        return item;
    }
}