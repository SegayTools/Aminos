using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ValueConverters;

public partial class ApplicationUrlConverter : IValueConverter
{
    private static readonly Regex templateRegex = new(@"\{(\d+)\}");
    private readonly HttpClient designClient;
    private readonly IApplicationHttpFactory factory;

    public ApplicationUrlConverter(IApplicationHttpFactory factory)
    {
        this.factory = factory;
        if (!DesignModeHelper.IsDesignMode)
            designClient = new HttpClient();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string urlTemplate)
            return default;

        if (value?.ToString() is not string templateValue)
            return default;

        var match = templateRegex.Match(urlTemplate);
        if (!match.Success)
            return default;

        var padLength = int.Parse(match.Groups[1].Value);
        var replVal = templateValue;
        if (padLength > 0)
            replVal = replVal.PadLeft(padLength,'0');

        var url = urlTemplate.Replace(match.Value, replVal);

        var obj = new Wrapper();

        GenerateRequestImageTask(url, obj);

        return obj;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private async void GenerateRequestImageTask(string url, Wrapper obj)
    {
        if (DesignModeHelper.IsDesignMode)
            return;

        var resp = await factory.SendAsync(url);
        var data = await resp.Content.ReadAsByteArrayAsync();
        if (data.Length == 0)
            return;
        var bitmap = new Bitmap(new MemoryStream(data));
        obj.Bitmap = bitmap;
    }

    private partial class Wrapper : ObservableObject
    {
        [ObservableProperty]
        private Bitmap bitmap;
    }
}