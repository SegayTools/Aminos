using System.Collections.ObjectModel;

namespace Aminos.Core.Utils.MethodExtensions;

public static class ObservableCollectionMethodExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
            list.Add(item);
    }
}