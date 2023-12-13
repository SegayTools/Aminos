using System;
using System.Collections.Generic;

namespace AminosUI.Utils.MethodExtensions;

public static class ListEx
{
    public static void SortBy<T>(this List<T> list, params Func<T, IComparable>[] keySelectors)
    {
        list.Sort((a, b) =>
        {
            foreach (var keySelector in keySelectors)
            {
                var ak = keySelector(a);
                var bk = keySelector(b);
                var cmp = ak.CompareTo(bk);

                if (cmp != 0)
                    return cmp;
            }

            return 0;
        });
    }
}