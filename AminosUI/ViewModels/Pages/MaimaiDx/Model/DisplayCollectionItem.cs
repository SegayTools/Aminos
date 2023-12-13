using Aminos.Core.Models.Title.SDEZ.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ViewModels.Pages.MaimaiDx.Model;

public partial class DisplayCollectionItem : ObservableObject
{
    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private string genre;

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private bool isEnable;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ItemKind type;

    [ObservableProperty]
    private string urlTemplate;
}