using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AminosUI.Controls.ControlTemplates;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Services.ViewModelFactory;
using AminosUI.ViewModels.Pages;
using AminosUI.ViewModels.Pages.Home;
using AminosUI.ViewModels.Pages.User;
using Avalonia.Notification;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace AminosUI.ViewModels;

public partial class MainViewModel : ViewModelBase, IApplicationNavigation
{
    private readonly ILogger<MainViewModel> logger;
    private readonly IApplicationNotification notification;
    private readonly IViewModelFactory viewModelFactory;

    [ObservableProperty]
    private bool isPaneOpen;

    [ObservableProperty]
    private ViewModelBase mainPageContent;

    [ObservableProperty]
    private ListItemTemplate selectedListItem;

    public MainViewModel(ILogger<MainViewModel> logger, IApplicationNotification notification,
        IServiceProvider serviceProvider,IViewModelFactory viewModelFactory)
    {
        this.logger = logger;
        this.notification = notification;
        this.viewModelFactory = viewModelFactory;

        NavigatePageAsync<HomePageViewModel>();
    }

    public INotificationMessageManager MessageManager => notification.MessageManager;

    public ObservableCollection<ListItemTemplate> TopItems { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "主页", "home-1391-svgrepo-com.svg"),
        new ListItemTemplate(typeof(UserInfoPageViewModel), "用户信息", "user-alt-1-svgrepo-com.svg"),
        new ListItemTemplate(typeof(MainViewModel), "maimai DX", "wash-machine-2-svgrepo-com.svg")
    };

    public ObservableCollection<ListItemTemplate> BottomItems { get; } = new();

    public async ValueTask NavigatePageAsync<T>(T existObj = default) where T : PageViewModelBase
    {
        var obj = existObj ?? viewModelFactory.CreateViewModel(typeof(T));
        var type = obj.GetType();

        MainPageContent = obj;

        if (TopItems.Concat(BottomItems).FirstOrDefault(x => x.ModelType == type) is ListItemTemplate template)
            //make select status if modelView type contains menu list.
            SelectedListItem = template;
    }

    public ValueTask NavigatePageAsync(Type pageViewModelType)
    {
        return NavigatePageAsync(viewModelFactory.CreateViewModel(pageViewModelType) as PageViewModelBase);
    }

    partial void OnSelectedListItemChanged(ListItemTemplate oldValue, ListItemTemplate newValue)
    {
        if (MainPageContent?.GetType() != newValue?.ModelType)
            MainPageContent = viewModelFactory.CreateViewModel(newValue?.ModelType);
    }

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}