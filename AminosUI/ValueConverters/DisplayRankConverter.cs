using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class DisplayRankConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int rank)
            rank = -2;
        rank++;
        return rank switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            -1 => "",
            _ => $"{rank}th"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}