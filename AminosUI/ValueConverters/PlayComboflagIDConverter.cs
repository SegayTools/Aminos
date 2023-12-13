using System;
using System.Collections.Generic;
using System.Globalization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class PlayComboflagIDConverter : IValueConverter
{
    private static readonly Dictionary<string, Bitmap> cache = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not PlayComboflagID flag)
            flag = PlayComboflagID.None;

        var path = flag switch
        {
            PlayComboflagID.Silver => "UI_MSS_MBase_Icon_FC_S.png",
            PlayComboflagID.Gold => "UI_MSS_MBase_Icon_FCp_S.png",
            PlayComboflagID.AllPerfect => "UI_MSS_MBase_Icon_AP_S.png",
            PlayComboflagID.AllPerfectPlus => "UI_MSS_MBase_Icon_APp_S.png",
            _ => "UI_MSS_MBase_Icon_Blank.png"
        };

        if (path is null)
            return default;

        path = "avares://AminosUI/Assets/UI_MSS_MBase/" + path;
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