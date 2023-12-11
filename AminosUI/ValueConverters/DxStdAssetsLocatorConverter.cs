using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AminosUI.ValueConverters;

public class DxStdAssetsLocatorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool val)
            val = true;

        var path = "avares://AminosUI" + (val
            ? "/Assets/UI_MSS_MBase/UI_UPE_Infoicon_DeluxeMode.png"
            : "/Assets/UI_MSS_MBase/UI_UPE_Infoicon_StandardMode.png");

        var uri = new Uri(path);
        var r = AssetLoader.Open(uri);
        return new Bitmap(r);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}