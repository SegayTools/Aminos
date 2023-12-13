using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class TitleRareImageConverter : IValueConverter
{
    private static readonly Dictionary<string, Bitmap> cache = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string rare)
            rare = "Bronze";
        if (string.IsNullOrWhiteSpace(rare))
            return default;

        var path = "avares://AminosUI/Assets/Title/UI_CMN_Shougou_" + rare + ".png";
        if (!cache.TryGetValue(path, out var bitmap))
        {
            var uri = new Uri(path);
            var r = AssetLoader.Open(uri);
            bitmap = new Bitmap(r);
            cache[path] = bitmap;
        }

        return bitmap;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}