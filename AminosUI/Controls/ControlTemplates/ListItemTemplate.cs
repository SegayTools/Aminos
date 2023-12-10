using System;
using Avalonia.Media;

namespace AminosUI.Controls.ControlTemplates;

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string label, string iconSymbol)
    {
        ModelType = type;
        Label = label ?? type.Name.Replace("PageViewModel", "");
        IconSymbol = iconSymbol;
    }

    public string Label { get; }
    public Type ModelType { get; }
    public string IconSymbol { get; }
}