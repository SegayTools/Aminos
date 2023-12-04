using System.Collections.Concurrent;
using Aminos.Core.Models.General;

namespace Aminos.Services.StatisticLoggers;

public abstract class CommonStatisticLoggerBase : IStatisticLogger
{
    private readonly ConcurrentDictionary<int, float> logValueMaps = new();

    public ValueTask AggregateValue(int type, int offsetValue)
    {
        if (!logValueMaps.TryGetValue(type, out var curValue))
            curValue = default;
        logValueMaps[type] = curValue + offsetValue;
        return ValueTask.CompletedTask;
    }

    public ValueTask SetValue(int type, int newValue)
    {
        logValueMaps[type] = newValue;
        return ValueTask.CompletedTask;
    }

    public ValueTask<IEnumerable<StatisticItem>> DumpStatistics()
    {
        var items = logValueMaps.Select(x => GenerateStatisticItem(x.Key, x.Value));
        return ValueTask.FromResult(items);
    }

    protected abstract StatisticItem GenerateStatisticItem(int type, float value);
}