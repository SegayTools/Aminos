﻿using AminosUI.ViewModels.Pages.MaimaiDx.Model;
using Avalonia;
using Avalonia.Controls;

namespace AminosUI.Controls;

public partial class MusicDisplayItemView : UserControl
{
    public enum DisplayDiffType
    {
        Basic = 0,
        Advanced = 1,
        Expert = 2,
        Master = 3,
        ReMaster = 4,
        Utage = 5
    }

    public static AvaloniaProperty DisplayDiffProperty =
        AvaloniaProperty.Register<MusicDisplayItemView, DisplayDiffType>(nameof(DisplayDiff),
            DisplayDiffType.Master, true);
    
    public static AvaloniaProperty MusicItemProperty =
        AvaloniaProperty.Register<MusicDisplayItemView, MusicDisplayItem>(nameof(MusicItem),
            default, true);

    public MusicDisplayItemView()
    {
        InitializeComponent();
        container.DataContext = this;
    }

    public DisplayDiffType DisplayDiff
    {
        get => (DisplayDiffType) GetValue(DisplayDiffProperty);
        set => SetValue(DisplayDiffProperty, value);
    }
    
    public MusicDisplayItem MusicItem
    {
        get => (MusicDisplayItem) GetValue(MusicItemProperty);
        set => SetValue(MusicItemProperty, value);
    }
}