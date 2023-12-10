using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Authorization;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Utils.MethodExtensions;
using AminosUI.Services.Applications;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using AminosUI.Utils.MethodExtensions;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace AminosUI.ViewModels.Pages.User;

public partial class UserInfoPageViewModel : PageViewModelBase
{
    private readonly ICardManager cardManager;
    private readonly IApplicationHttpFactory httpFactory;
    private readonly IKeychipManager keychipManager;
    private readonly ILogger<UserInfoPageViewModel> logger;
    private readonly IApplicationNavigation navigation;
    private readonly IApplicationNotification notification;
    private readonly IUserManager userManager;

    [ObservableProperty]
    public ObservableCollection<Activity> activities = new();

    [ObservableProperty]
    public ObservableCollection<Card> cards = new();

    [ObservableProperty]
    public ObservableCollection<Keychip> keychips = new()
    {
        new Keychip
        {
            Id = "KRQOIEIF",
            RegisterDate = DateTime.Now,
            LastAccessDate = DateTime.Now,
            Name = "qweqrio",
            Enable = true
        },
        new Keychip
        {
            Id = "KRQOIEIF",
            RegisterDate = DateTime.Now,
            LastAccessDate = DateTime.Now,
            Name = "qweqrio",
            Enable = true
        }
    };

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsKeyichipListVisible))]
    private UserAccount user;

    public UserInfoPageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();
    }

    public UserInfoPageViewModel(
        ILogger<UserInfoPageViewModel> logger,
        IUserManager userManager,
        IKeychipManager keychipManager,
        ICardManager cardManager,
        IApplicationHttpFactory httpFactory,
        IApplicationNotification notification,
        IApplicationNavigation navigation
    )
    {
        this.logger = logger;
        this.userManager = userManager;
        this.keychipManager = keychipManager;
        this.cardManager = cardManager;
        this.httpFactory = httpFactory;
        this.notification = notification;
        this.navigation = navigation;
    }

    public bool IsKeyichipListVisible => user?.Role >= AuthRolePolicy.Admin;

    public override string Title => "用户信息";

    public async void RefreshRecentPlayStatistics()
    {
        if (userManager?.CurrentUser is not UserAccount userAccount)
            return;
    }

    public async void RefreshActivities()
    {
        if (userManager?.CurrentUser is not UserAccount userAccount)
            return;

        User = userAccount;
        using var dis = notification.BeginLoadingNotification("获取数据", out var cancellation);

        #region Activities

        {
            Activities.Clear();
            var takeCount = 50;

            var resp = await httpFactory.GetAsCommonApi<Activity[]>("api/Account/GetActivities", new
            {
                takeCount,
                skipCount = 0
            }, cancellation);

            if (resp.isSuccess)
            {
                if (resp.obj is Activity[] arr)
                    foreach (var activity in arr)
                        Activities.Add(activity);
            }
            else
            {
                notification.ShowError($"无法获取Activity数据:{resp.message}");
            }
        }

        #endregion

        #region Cards

        RefreshCardsInternal(cancellation);

        #endregion

        #region Keychips(if able)

        if (User.Role >= AuthRolePolicy.Admin)
        {
            Keychips.Clear();
            var resp = await keychipManager.GetKeychips();
            if (resp.isSuccess)
                Keychips.AddRange(resp.obj);
            else
                notification.ShowError($"无法获取Keychip数据:{resp.message}");
        }

        #endregion
    }

    public override void OnViewAfterLoaded(Control control)
    {
        base.OnViewAfterLoaded(control);

        if (userManager.CurrentUser is null)
        {
            navigation.NavigatePageAsync<UserLoginPageViewModel>();
            notification.ShowWarnning("请先登录");
            return;
        }

        RefreshActivities();
        RefreshRecentPlayStatistics();
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task UnbindCard(Card card, CancellationToken token)
    {
        using var disp = notification.BeginLoadingNotification("正在解绑卡片", out var cancellationToken);
        var resp = await cardManager.UnbindCard(card.AccessCode, cancellationToken);

        if (resp.isSuccess)
        {
            notification.ShowInfomation("解绑成功");
            RefreshCards(token);
        }
        else
        {
            notification.ShowError($"解绑失败:{resp.message}");
        }
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task BindCard(string accessCode, CancellationToken token)
    {
        using var disp = notification.BeginLoadingNotification("正在绑定卡片", out var cancellationToken);
        var resp = await cardManager.BindCard(accessCode, cancellationToken);

        if (resp.isSuccess)
        {
            notification.ShowInfomation("绑定成功");
            RefreshCards(token);
        }
        else
        {
            notification.ShowError($"绑定失败:{resp.message}");
        }
    }

    private async Task RefreshCardsInternal(CancellationToken token)
    {
        Cards.Clear();
        var resp = await cardManager.GetCards(token);
        if (resp.isSuccess)
            Cards.AddRange(resp.obj);
        else
            notification.ShowError($"无法获取Card数据:{resp.message}");
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task RefreshCards(CancellationToken token)
    {
        using var disp2 = notification.BeginLoadingNotification("刷新卡片列表", out var cancellationToken2);
        RefreshCardsInternal(cancellationToken2);
    }

    private async Task RefreshKeychipsInternal(CancellationToken token)
    {
        Keychips.Clear();
        var resp = await keychipManager.GetKeychips(token);
        if (resp.isSuccess)
            Keychips.AddRange(resp.obj);
        else
            notification.ShowError($"无法获取Keychip数据:{resp.message}");
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task RefreshKeychips(CancellationToken token)
    {
        using var disp2 = notification.BeginLoadingNotification("刷新狗号列表", out var cancellationToken2);
        RefreshKeychipsInternal(cancellationToken2);
    }

    [RelayCommand]
    private async Task GenerateKeychip(string keychip, CancellationToken token)
    {
        if (!string.IsNullOrWhiteSpace(keychip))
            keychip = string.Concat(keychip.ToUpper().Where(x => x switch
            {
                (>= 'A' and <= 'Z') or (>= '0' and <= '9') => true,
                _ => false
            }));
        else
            keychip = null;

        using var disp = notification.BeginLoadingNotification("正在生成狗号", out var cancellationToken);
        var resp = await keychipManager.GenerateNewKeychip(keychip, cancellationToken);
        if (resp.isSuccess)
            notification.ShowInfomation("生成狗号成功");
        else
            notification.ShowInfomation($"生成狗号失败:{resp.message}");

        RefreshKeychips(cancellationToken);
    }

    [RelayCommand]
    private async Task RemoveKeychips(IList keychips, CancellationToken token)
    {
        var selectedKeychips = keychips.OfType<Keychip>().ToArray();
        if (selectedKeychips.Length == 0)
        {
            notification.ShowWarnning("并未选择要删除的狗号");
            return;
        }

        using var disp = notification.BeginLoadingNotification("正在删除已选狗号", out var cancellationToken);
        var allGood = true;
        foreach (var keychip in selectedKeychips)
        {
            var resp = await keychipManager.RemoveKeychip(keychip, cancellationToken);
            if (!resp.isSuccess)
            {
                notification.ShowError($"无法删除狗号{keychip.Id}:{resp.message}");
                allGood = false;
            }
        }

        if (allGood)
            notification.ShowInfomation("已选狗号删除成功");
        else
            notification.ShowWarnning("部分狗号删除成功");

        RefreshKeychips(cancellationToken);
    }

    [RelayCommand]
    private async Task UpdateKeychips(CancellationToken token)
    {
        using var disp = notification.BeginLoadingNotification("正在保存更改", out var cancellationToken);
        var allGood = true;
        foreach (var keychip in Keychips)
        {
            var resp = await keychipManager.UpdateKeychip(keychip, cancellationToken);
            if (!resp.isSuccess)
            {
                notification.ShowError($"无法保存狗号{keychip.Id}的变更内容:{resp.message}");
                allGood = false;
            }
        }

        if (allGood)
            notification.ShowInfomation("狗号信息保存成功");
        else
            notification.ShowWarnning("部分狗号信息保存成功");

        RefreshKeychips(cancellationToken);
    }
}