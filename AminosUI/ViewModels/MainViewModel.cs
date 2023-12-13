using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AminosUI.Controls.ControlTemplates;
using AminosUI.Services.Applications;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Services.Persistences;
using AminosUI.Services.ViewModelFactory;
using AminosUI.Utils;
using AminosUI.ViewModels.Pages;
using AminosUI.ViewModels.Pages.MaimaiDx;
using AminosUI.ViewModels.Pages.User;
using Avalonia.Notification;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using HomePageViewModel = AminosUI.ViewModels.Pages.Home.HomePageViewModel;

namespace AminosUI.ViewModels;

public partial class MainViewModel : ViewModelBase, IApplicationNavigation
{
    private readonly ILocalStoreDataPersistence localStore;
    private readonly ILogger<MainViewModel> logger;
    private readonly IApplicationNotification notification;
    private readonly IUserManager userManager;
    private readonly IViewModelFactory viewModelFactory;

    [ObservableProperty]
    private bool enableNavigratable;

    [ObservableProperty]
    private bool isPaneOpen;

    [ObservableProperty]
    private ViewModelBase mainPageContent;

    [ObservableProperty]
    private ListItemTemplate selectedListItem;

    public MainViewModel(
        ILogger<MainViewModel> logger,
        IApplicationNotification notification,
        IServiceProvider serviceProvider,
        IViewModelFactory viewModelFactory,
        ILocalStoreDataPersistence localStore,
        IUserManager userManager)
    {
        this.logger = logger;
        this.notification = notification;
        this.viewModelFactory = viewModelFactory;
        this.localStore = localStore;
        this.userManager = userManager;

        ProcessInit();
    }

    public INotificationMessageManager MessageManager => notification.MessageManager;

    public ObservableCollection<ListItemTemplate> TopItems { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "主页", "Home"),
        new ListItemTemplate(typeof(UserInfoPageViewModel), "用户信息", "Person"),
        new ListItemTemplate(typeof(Pages.MaimaiDx.HomePageViewModel), "maimai DX 主页", "Games"),
        new ListItemTemplate(typeof(MusicListPageViewModel), "maimai Dx 曲库", "MusicNote1"),
        new ListItemTemplate(typeof(CollectionLookupPageViewModel), "maimai Dx 收藏品", "CollectionsAdd")
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

    private async void ProcessInit()
    {
        if (!DesignModeHelper.IsDesignMode)
        {
            var setting = await localStore.Load<ApplicationSettings>(nameof(ApplicationSettings));
            if (setting.EnableAutoLogin && userManager.CurrentUser is null)
            {
                using var disp = notification.BeginLoadingNotification("自动登录中", out var cancellationToken);
                if (!string.IsNullOrWhiteSpace(setting.Cookies))
                {
                    var collection = JsonSerializer.Deserialize<CookieCollection>(setting.Cookies);
                    var container = new CookieContainer();
                    container.Add(collection);

                    var resp = await userManager.LoginByCookies(container, cancellationToken);
                    if (!resp.isSuccess)
                    {
                        setting.EnableAutoLogin = default;
                        setting.Cookies = default;
                        await localStore.Save(nameof(ApplicationSettings), setting);

                        notification.ShowWarnning("无法自动登录，因为用户凭证已失效");
                    }
                }
                else
                {
                    notification.ShowWarnning("无法自动登录，因为没保存用户凭证数据");
                }
            }
        }

        NavigatePageAsync<HomePageViewModel>();
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