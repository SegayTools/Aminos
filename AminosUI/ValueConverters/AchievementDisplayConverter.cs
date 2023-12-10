using System;
using System.Globalization;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class AchievementDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not CalculatedRating calculatedRating)
            return default;
        return calculatedRating.MusicDetail.achievement / 10000.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}