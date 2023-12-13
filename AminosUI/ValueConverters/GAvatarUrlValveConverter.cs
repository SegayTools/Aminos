using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using AminosUI.Utils;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ValueConverters;

public partial class GAvatarUrlValveConverter : IValueConverter
{
    private readonly HttpClient client = new();
    private readonly SHA256 sha256 = SHA256.Create();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var email = value?.ToString() ?? string.Empty;
        var hash = System.Convert
            .ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(email.Trim().ToLowerInvariant())))
            .ToLowerInvariant();

        var size = parameter switch
        {
            int s => s,
            string str => int.Parse(str),
            _ => 128
        };

        var url = $"https://gravatar.com/avatar/{hash}?size={size}";

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

        var data = await client.GetByteArrayAsync(url, default);
        if ((data?.Length ?? 0) == 0)
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