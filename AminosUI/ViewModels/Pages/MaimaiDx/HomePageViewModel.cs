using System;
using Aminos.Core.Models.Title.SDEZ.Tables;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using AminosUI.Utils;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ViewModels.Pages.MaimaiDx;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly IApplicationHttpFactory httpFactory;
    private readonly IApplicationNavigation navigation;

    [ObservableProperty]
    private UserDetail userDetail;

    [ObservableProperty]
    private UserExtend userExtend;

    [ObservableProperty]
    private UserOption userOption;

    public HomePageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();

        UserDetail = new UserDetail()
        {
            userName = "MikiraSora",
            firstDataVersion = "3.5"
        };
        UserExtend = new UserExtend();
        UserOption = new UserOption();
    }

    public HomePageViewModel(IApplicationHttpFactory httpFactory, IApplicationNavigation navigation)
    {
        this.httpFactory = httpFactory;
        this.navigation = navigation;
    }

    public override string Title => "Maimai玩家个人主页";

    private async void RefreshPage()
    {
        
    }

    public override void OnViewAfterLoaded(Control control)
    {
        base.OnViewAfterLoaded(control);
        RefreshPage();
    }
}