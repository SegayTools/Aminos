using Aminos.Core.Models.Title.SDEZ.Tables;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ViewModels.Pages.MaimaiDx;

public partial class SettingPageViewModel : PageViewModelBase
{
    private readonly IApplicationNavigation applicationNavigation;
    private readonly IApplicationHttpFactory httpFactory;

    [ObservableProperty]
    private UserOption userOption;

    public SettingPageViewModel(IApplicationNavigation applicationNavigation, IApplicationHttpFactory httpFactory)
    {
        this.applicationNavigation = applicationNavigation;
        this.httpFactory = httpFactory;
    }

    public override string Title => "个人设置";
}