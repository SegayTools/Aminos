using Aminos.Core.Models.General;

namespace Aminos.Services.StatisticLoggers;

public interface IStatisticLogger
{
    ValueTask<IEnumerable<StatisticItem>> DumpStatistics();
}