using System;
using System.Globalization;
using System.Linq;
using AminosUI.Controls;
using AminosUI.ViewModels.Pages.MaimaiDx.Model;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class DiffDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var showSymbolCheck = false;
        if (parameter is string templateStr && bool.Parse(templateStr))
            showSymbolCheck = true;

        var level = 0;
        var levelDecimal = 0;

        if (value is MusicDisplayItem item)
        {
            var note = item.MusicData.NotesData?.Notes?.ElementAtOrDefault(item.DifficultyId switch
            {
                MusicDisplayItemView.DisplayDiffType.Utage => 0,
                _ => (int) item.DifficultyId
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

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}