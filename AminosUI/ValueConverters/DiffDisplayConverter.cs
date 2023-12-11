using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AminosUI.Controls;
using AminosUI.ViewModels.Pages.MaimaiDx.Model;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class DiffDisplayConverter : IMultiValueConverter
{
    public object Convert(IList<object> value, Type targetType, object parameter, CultureInfo culture)
    {
        var showSymbolCheck = false;
        if (parameter is string templateStr && bool.Parse(templateStr))
            showSymbolCheck = true;

        var level = 0;
        var levelDecimal = 0;

        if (value.Count > 2 && value[0] is MusicDisplayItem item &&
            value[1] is MusicDisplayItemView.DisplayDiffType diffType)
        {
            var note = item.Data.NotesData?.Notes?.ElementAtOrDefault(diffType switch
            {
                MusicDisplayItemView.DisplayDiffType.Utage => 0,
                _ => (int) diffType
            });
        }

        if (showSymbolCheck)
        {
            if (levelDecimal > 50)
                return "+";
        }
        else
        {
            return level;
        }

        return default;
    }

    public object ConvertBack(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}