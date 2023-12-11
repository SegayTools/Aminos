using System;
using System.Globalization;
using System.Linq;
using AminosUI.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class DiffAssetsLocatorConverter : IValueConverter
{
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
        var uri = new Uri(path);
        var r = AssetLoader.Open(uri);
        return new Bitmap(r);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}