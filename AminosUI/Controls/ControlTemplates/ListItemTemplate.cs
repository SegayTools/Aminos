using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using System;

namespace AminosUI.Controls.ControlTemplates;

public class ListItemTemplate
{
	private readonly string assetSvgFileName;

	public ListItemTemplate(Type type, string label, string assetSvgFileName = "")
	{
		ModelType = type;
		Label = label ?? type.Name.Replace("PageViewModel", "");
		this.assetSvgFileName = assetSvgFileName;
	}

	public string Label { get; }
	public Type ModelType { get; }
	public string Svg => "/Assets/" + assetSvgFileName;
}
