using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Utils.MethodExtensions;
using AminosUI.Services.Applications;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using AminosUI.ViewModels.Pages.User;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AminosUI.ViewModels.Pages.MaimaiDx;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly ICardManager cardManager;
    private readonly IApplicationHttpFactory httpFactory;
    private readonly IApplicationNavigation navigation;
    private readonly IApplicationNotification notification;
    private readonly ISdezDataManager sdezDataManager;
    private readonly IUserManager userManager;

    [ObservableProperty]
    private ObservableCollection<RivalItem> rivals = new();

    [ObservableProperty]
    private UserDetail userDetail;

    [ObservableProperty]
    private UserExtend userExtend;

    [ObservableProperty]
    private UserOption userOption;

    public HomePageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();

        UserDetail = new UserDetail
        {
            userName = "MikiraSora",
            firstDataVersion = "3.5"
        };
        UserExtend = new UserExtend();
        UserOption = new UserOption();
    }

    public HomePageViewModel(
        IApplicationHttpFactory httpFactory,
        IApplicationNavigation navigation,
        IApplicationNotification notification,
        ISdezDataManager sdezDataManager,
        IUserManager userManager,
        ICardManager cardManager)
    {
        this.httpFactory = httpFactory;
        this.navigation = navigation;
        this.notification = notification;
        this.sdezDataManager = sdezDataManager;
        this.userManager = userManager;
        this.cardManager = cardManager;
    }

    public ObservableCollection<CalculatedRating> Ratings { get; } = new();
    public ObservableCollection<CalculatedRating> NextRatings { get; } = new();

    public override string Title => "Maimai玩家个人主页";

    private async void RefreshPage()
    {
        if (userManager.CurrentUser is null)
        {
            navigation.NavigatePageAsync<UserLoginPageViewModel>();
            notification.ShowWarnning("请先登录");
            return;
        }

        //todo 先当作只有一个账号来设计

        using var disp = notification.BeginLoadingNotification("获取玩家数据中", out var cancellationToken);

        var cardsResp = await cardManager.GetCards(cancellationToken);
        if (!cardsResp.isSuccess)
        {
            notification.ShowError($"获取Card数据失败:{cardsResp.message}");
            return;
        }

        if (cardsResp.obj.Length == 0)
        {
            notification.ShowError("用户并未游玩此游戏，或者游玩的Aime卡并未绑定到此用户上");
            return;
        }

        var userDetail = default(UserDetail);
        var userId = 0UL;
        foreach (var card in cardsResp.obj)
        {
            userId = (ulong) card.AimeId;
            userDetail = await sdezDataManager.GetUserDetail(userId, cancellationToken);
            if (userDetail is not null)
                break;
        }

        if (userDetail is null)
        {
            notification.ShowWarnning("用户并未游玩此游戏，或者游玩的Aime卡并未绑定到此用户上");
            return;
        }

        var userOption = await sdezDataManager.GetUserOption(userId, cancellationToken);
        if (userOption is null)
        {
            notification.ShowError("获取UserOption数据失败");
            return;
        }

        var userExtend = await sdezDataManager.GetUserExtend(userId, cancellationToken);
        if (userExtend is null)
        {
            notification.ShowError("获取UserExtend数据失败");
            return;
        }

        Ratings.Clear();
        NextRatings.Clear();
        var ratings = await sdezDataManager.GetCalculatedRatingResponse(userId, cancellationToken);
        if (ratings?.RatingList != null)
            Ratings.AddRange(ratings.RatingList);
        if (ratings?.NextRatingList != null)
            NextRatings.AddRange(ratings.NextRatingList);

        UserDetail = userDetail;
        UserOption = userOption;
        UserExtend = userExtend;
        await RefreshRivalInternal(cancellationToken);
    }

    private async Task RefreshRivalInternal(CancellationToken cancellationToken)
    {
        var rivals = await sdezDataManager.GetUserRivals(UserDetail.Id, cancellationToken);
        if (rivals is null)
        {
            notification.ShowError("获取UserRivals数据失败");
            return;
        }

        Rivals.Clear();
        foreach (var rivalUserDetail in rivals)
            Rivals.Add(new RivalItem
            {
                RivalUserId = rivalUserDetail.Id,
                LastPlayedDateTime = rivalUserDetail.lastLoginDate,
                UserName = rivalUserDetail.userName
            });
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task DeleteRivals(IList list, CancellationToken token)
    {
        var rivalItems = list.OfType<RivalItem>().ToArray();
        if (rivalItems.Length == 0)
        {
            notification.ShowWarnning("Rival列表并未选择");
            return;
        }

        var allGood = true;
        {
            using var disp = notification.BeginLoadingNotification("删除Rival", out var cancellationToken);
            foreach (var item in rivalItems)
            {
                var resp = await sdezDataManager.DeleteRival(UserDetail.Id, item.RivalUserId, cancellationToken);
                if (!resp.isSuccess)
                    allGood = false;
            }
        }

        if (!allGood)
            notification.ShowWarnning("部分删除Rival请求失败");
        else
            notification.ShowInfomation("删除成功");

        await RefreshRivalInternal(token);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task AddRival(string rivalUserIdStr, CancellationToken token)
    {
        var rivalUserId = ulong.Parse(rivalUserIdStr);
        {
            using var disp = notification.BeginLoadingNotification("添加Rival", out var cancellationToken);
            var resp = await sdezDataManager.AddRival(UserDetail.Id, rivalUserId, cancellationToken);
            if (!resp.isSuccess)
                notification.ShowWarnning($"添加失败:{resp.message}");
            else
                notification.ShowInfomation("添加成功");
        }

        await RefreshRivalInternal(token);
    }

    public override void OnViewAfterLoaded(Control control)
    {
        base.OnViewAfterLoaded(control);
        RefreshPage();
    }

    public class RivalItem
    {
        public ulong RivalUserId { get; set; }
        public string UserName { get; set; }
        public DateTime LastPlayedDateTime { get; set; }
    }
}