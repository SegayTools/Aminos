using System;
using System.Collections.Generic;
using System.Globalization;
using AminosUI.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class DiffAssetsLocatorConverter : IValueConverter
{
    private static readonly Dictionary<string, Bitmap> cache = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string templateStr)
            return default;

        if (value is not MusicDisplayItemView.DisplayDiffType diff)
            diff = MusicDisplayItemView.DisplayDiffType.Master;

        var val = diff switch
        {
            MusicDisplayItemView.DisplayDiffType.Basic => "BSC",
            MusicDisplayItemView.DisplayDiffType.Advanced => "ADV",
            MusicDisplayItemView.DisplayDiffType.Expert => "EXP",
            MusicDisplayItemView.DisplayDiffType.Master => "MST",
            MusicDisplayItemView.DisplayDiffType.ReMaster => "MST_Re",
            MusicDisplayItemView.DisplayDiffType.Utage => "UTG",
            _ => throw new ArgumentOutOfRangeException()
        };

        var path = "avares://AminosUI/" + templateStr.Replace("BSC", val);
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