using System;
using System.Collections.Generic;
using System.Globalization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class PlaySyncflagIDConverter : IValueConverter
{
    private static readonly Dictionary<string, Bitmap> cache = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not PlaySyncflagID flag)
            flag = PlaySyncflagID.None;

        var path = flag switch
        {
            PlaySyncflagID.ChainLow => "UI_MSS_MBase_Icon_FS_S.png",
            PlaySyncflagID.ChainHi => "UI_MSS_MBase_Icon_FSp_S.png",
            PlaySyncflagID.SyncLow => "UI_MSS_MBase_Icon_FSD_S.png",
            PlaySyncflagID.SyncHi => "UI_MSS_MBase_Icon_FSDp_S.png",
            PlaySyncflagID.SyncPlay => "UI_MSS_MBase_Icon_SP_S.png",
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