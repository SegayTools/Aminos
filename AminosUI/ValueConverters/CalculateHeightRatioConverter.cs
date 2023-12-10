using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class CalculateHeightRatioConverter : IValueConverter
{
    private static IDictionary<string, float> cached = new Dictionary<string, float>();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string rstr)
            return value;
        if (value is not double width)
            return value;

        if (!cached.TryGetValue(rstr,out var ratio))
        {
            var r = rstr.Split("x");
            ratio = float.Parse(r[0]) / float.Parse(r[1]);

            cached[rstr] = ratio;
        }

        return width / ratio;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}