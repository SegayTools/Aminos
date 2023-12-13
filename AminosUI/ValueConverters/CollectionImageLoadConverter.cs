using System;
using System.Globalization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using AminosUI.ViewModels.Pages.MaimaiDx.Model;
using Avalonia.Data.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace AminosUI.ValueConverters;

public class CollectionImageLoadConverter : IValueConverter
{
    private readonly AssetsImageLoadConverter assetsImageLoadConverter;

    public CollectionImageLoadConverter(IServiceProvider provider)
    {
        assetsImageLoadConverter = ActivatorUtilities.CreateInstance<AssetsImageLoadConverter>(provider);
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DisplayCollectionItem item)
            return default;
        if (item.Type == ItemKind.Title)
            return default;
        return assetsImageLoadConverter.Convert(item.Id, targetType, item.UrlTemplate, culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}