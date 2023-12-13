using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class IsNullCheckConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var reverse = false;
        if (parameter is bool r)
            reverse = r;
        if (parameter is string s)
            reverse = bool.Parse(s);
        var val = value is null;
        return reverse ? !val : val;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}