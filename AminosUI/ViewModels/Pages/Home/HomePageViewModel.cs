using System;
using System.Collections.ObjectModel;
using System.Linq;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Utils.MethodExtensions;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils;
using AminosUI.Utils.MethodExtensions;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AminosUI.ViewModels.Pages.Home;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly IApplicationHttpFactory httpFactory;

    [ObservableProperty]
    private ObservableCollection<Announcement> announcements = new();

    [ObservableProperty]
    private StatisticItem sdezUserOnlineStatisticItem = new();

    [ObservableProperty]
    private StatisticItem serverRunningStatisticItem = new();

    [ObservableProperty]
    private StatisticItem uiVisitedStatisticItem = new();

    [ObservableProperty]
    private StatisticItem userRecentStatisticItem = new();

    [ObservableProperty]
    private StatisticItem userRegisteredStatisticItem = new();

    public HomePageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();

        Announcements.AddRange(new[]
        {
            new Announcement
            {
                Title = "ATitle1",
                Time = DateTime.Now,
                UserAccount = new UserAccount
                {
                    Name = "Author1"
                },
                Content = "asdqweqweqweqwe"
            },
            new Announcement
            {
                Title = "ATitle2",
                Time = DateTime.Now,
                UserAccount = new UserAccount
                {
                    Name = "Author1"
                },
                Content = "asdqweqweqweqwe"
            }
        });

        serverRunningStatisticItem = new StatisticItem
        {
            Value = 152000000
        };
    }

    public HomePageViewModel(IApplicationHttpFactory httpFactory)
    {
        this.httpFactory = httpFactory;
    }

    public override string Title => "主页";

    private async void RefreshPage()
    {
        #region Announcements

        var announcementArr = await httpFactory.GetAsCommonApi<Announcement[]>("api/General/GetAnnouncements",
            new
            {
                takeCount = 10,
                skipCount = 0
            });
        Announcements.AddRange(announcementArr?.obj ?? Enumerable.Empty<Announcement>());

        #endregion

        #region Statistic

        var generalStatistic =
            await httpFactory.GetAsCommonApi<StatisticItem[]>("api/General/GetGeneralStatistic");

        if (generalStatistic.isSuccess)
        {
            UserRecentStatisticItem =
                generalStatistic.obj.FirstOrDefault(x => x.ValueType == 3) ?? UserRecentStatisticItem;
            UserRegisteredStatisticItem =
                generalStatistic.obj.FirstOrDefault(x => x.ValueType == 0) ?? UserRegisteredStatisticItem;
            ServerRunningStatisticItem =
                generalStatistic.obj.FirstOrDefault(x => x.ValueType == 1) ?? ServerRunningStatisticItem;
            UiVisitedStatisticItem =
                generalStatistic.obj.FirstOrDefault(x => x.ValueType == 2) ?? UiVisitedStatisticItem;
        }

        var gameStatistic =
            await httpFactory.GetAsCommonApi<StatisticItem[]>("api/General/GetGameStatistic");
        if (gameStatistic.isSuccess)
            SdezUserOnlineStatisticItem =
                gameStatistic.obj.FirstOrDefault(x => x.ValueType == 0) ?? SdezUserOnlineStatisticItem;

        #endregion
    }

    public override void OnViewAfterLoaded(Control control)
    {
        base.OnViewAfterLoaded(control);
        RefreshPage();
    }

    [RelayCommand]
    private void ClickAnnouncement(Announcement announcement)
    {
        //todo
    }
}