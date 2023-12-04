using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AminosUI.ValueConverters;

public class ServerRunningTimeDisplayerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var timeSpan = TimeSpan.FromMilliseconds((float) value);
        var str = "";

        var days = (int) timeSpan.TotalDays;
        if (days > 0)
            str += $"{days}天";
        var hours = timeSpan.Hours;
        if (hours > 0)
            str += $"{hours}小时";

        if (string.IsNullOrWhiteSpace(str))
            str = "刚启动的";

        return str;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}