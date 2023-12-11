using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using AminosUI.Controls;
using AminosUI.Services.Applications;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using AminosUI.ViewModels.Pages.MaimaiDx.Model;
using AminosUI.ViewModels.Pages.User;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AminosUI.ViewModels.Pages.MaimaiDx;

public partial class MusicListPageViewModel : PageViewModelBase
{
    private readonly ICardManager cardManager;
    private readonly IApplicationHttpFactory httpFactory;
    private readonly IApplicationNavigation navigation;
    private readonly IApplicationNotification notification;
    private readonly ISdezDataManager sdezDataManager;
    private readonly IUserManager userManager;

    private Dictionary<MusicDifficultyID, CompositeUserMusicDetail[]> cachedMusicDetailRanks;
    private UserMusicDetail[] cachedUserMusicDetails;

    private CancellationTokenSource currentCancelSource;

    [ObservableProperty]
    private MusicData.Note currentNote;

    [ObservableProperty]
    private string currentUserRank;

    [ObservableProperty]
    private MusicDisplayItemView.DisplayDiffType displayDiff;

    [ObservableProperty]
    private CompositeUserMusicDetail[] musicDetailRankList;

    [ObservableProperty]
    private ObservableCollection<MusicItemGroup> musicGroups = new();

    [ObservableProperty]
    private MusicDisplayItem selectedItem;

    [ObservableProperty]
    private UserDetail userDetail;

    [ObservableProperty]
    private UserMusicDetail userMusicDetail;

    public MusicListPageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();

        UserDetail = new UserDetail
        {
            userName = "MikiraSora",
            firstDataVersion = "3.5"
        };

        UpdateCurrentUserMusicDetail();
        UpdateRank(default);
    }

    public MusicListPageViewModel(IApplicationHttpFactory httpFactory,
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

        RefreshPage();
    }

    public MusicDifficultyID DisplayMusicDifficultyId => DisplayDiff switch
    {
        MusicDisplayItemView.DisplayDiffType.Basic => MusicDifficultyID.Basic,
        MusicDisplayItemView.DisplayDiffType.Advanced => MusicDifficultyID.Advanced,
        MusicDisplayItemView.DisplayDiffType.Expert => MusicDifficultyID.Expert,
        MusicDisplayItemView.DisplayDiffType.Master => MusicDifficultyID.Master,
        MusicDisplayItemView.DisplayDiffType.ReMaster => MusicDifficultyID.ReMaster,
        MusicDisplayItemView.DisplayDiffType.Utage => MusicDifficultyID.Strong
    };

    public override string Title { get; } = "Maimai乐曲浏览器";

    private async void RefreshPage()
    {
        if (userManager.CurrentUser is null)
        {
            navigation.NavigatePageAsync<UserLoginPageViewModel>();
            notification.ShowWarnning("请先登录");
            return;
        }

        //todo 先当作只有一个账号来设计
        using var disp = notification.BeginLoadingNotification("获取乐曲数据中", out var cancellationToken);
        await RefreshMusicGroupsInternal(cancellationToken);
    }

    private async ValueTask RefreshMusicGroupsInternal(CancellationToken cancellationToken)
    {
        var resp = await sdezDataManager.GetAllMusicData(cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法获取曲库列表:{resp.message}");
            return;
        }

        MusicGroups.Clear();
        var items = resp.obj.Select(x => new MusicDisplayItem
        {
            Data = x
        }).ToArray();
        var defaultGroup = new MusicItemGroup
        {
            Name = "未分类的",
            Id = -1
        };

        defaultGroup.Items = items;

        MusicGroups.Add(defaultGroup);

        if (SelectedItem != null)
            SelectedItem = items.FirstOrDefault(x => x.Data.Id == SelectedItem.Data.Id);
    }

    async partial void OnSelectedItemChanged(MusicDisplayItem oldValue, MusicDisplayItem newValue)
    {
        currentCancelSource?.Cancel();
        currentCancelSource = new CancellationTokenSource();
        using var disp = notification.BeginLoadingNotification("获取详细列表", out var cancellationToken);
        var actualCancel =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, currentCancelSource.Token);

        var task1 = RefreshUserMusicDetail(newValue, actualCancel.Token);
        var task2 = RefreshUserRank(newValue, actualCancel.Token);

        await Task.WhenAll(task1, task2);

        UpdateCurrentUserMusicDetail();
        await UpdateRank(cancellationToken);
    }

    [RelayCommand]
    private async Task MusicDisplayItemTapped(MusicDisplayItem item)
    {
        SelectedItem = item;
    }

    async partial void OnDisplayDiffChanged(MusicDisplayItemView.DisplayDiffType oldValue,
        MusicDisplayItemView.DisplayDiffType newValue)
    {
        UpdateCurrentUserMusicDetail();
        await UpdateRank(default);
    }

    private async ValueTask UpdateRank(CancellationToken cancellationToken)
    {
        int BinarySearchIndex(CompositeUserMusicDetail[] array, UserMusicDetail target)
        {
            var left = 0;
            var right = array.Length - 1;

            while (left <= right)
            {
                var mid = left + (right - left) / 2;

                if (array[mid].UserMusicDetail.deluxscoreMax == target.deluxscoreMax)
                    return mid; // 找到目标值，返回索引
                if (array[mid].UserMusicDetail.deluxscoreMax < target.deluxscoreMax)
                    left = mid + 1; // 目标值在右半部分
                else
                    right = mid - 1; // 目标值在左半部分
            }

            return -1; // 没有找到目标值
        }

        MusicDetailRankList = cachedMusicDetailRanks?.TryGetValue(DisplayMusicDifficultyId, out var arr) ?? false
            ? arr
            : new CompositeUserMusicDetail[0];
        if (UserMusicDetail is null)
        {
            CurrentUserRank = "?";
        }
        else
        {
            var idx = BinarySearchIndex(MusicDetailRankList, UserMusicDetail);
            CurrentUserRank = idx == -1 ? "?" : idx.ToString();
        }
    }

    private async Task RefreshUserRank(MusicDisplayItem newValue, CancellationToken cancellationToken)
    {
        var musicId = newValue.Data.Id;

        var resp = await sdezDataManager.GetMusicDetailRank(20, DisplayMusicDifficultyId, 0, musicId,
            cancellationToken);
        if (resp.isSuccess)
            cachedMusicDetailRanks = resp.obj.GroupBy(x => x.UserMusicDetail.level)
                .ToDictionary(x => x.Key, x => x.OrderBy(x => x.UserMusicDetail.deluxscoreMax).ToArray());
        else
            notification.ShowError($"无法加载排行榜数据:{resp.message}");
    }

    private async Task RefreshUserMusicDetail(MusicDisplayItem newValue, CancellationToken cancellationToken)
    {
        var musicId = newValue.Data.Id;

        var resp = await sdezDataManager.GetUserMusicDetail(userDetail.Id, musicId, cancellationToken);
        if (resp.isSuccess)
            cachedUserMusicDetails = resp.obj;
        else
            notification.ShowError($"无法加载用户歌曲成绩数据:{resp.message}");
        UpdateCurrentUserMusicDetail();
    }

    private void UpdateCurrentUserMusicDetail()
    {
        UserMusicDetail = cachedUserMusicDetails?.FirstOrDefault(x => x.level == DisplayMusicDifficultyId);

        var noteIdx = DisplayDiff switch
        {
            MusicDisplayItemView.DisplayDiffType.Basic => 0,
            MusicDisplayItemView.DisplayDiffType.Advanced => 1,
            MusicDisplayItemView.DisplayDiffType.Expert => 2,
            MusicDisplayItemView.DisplayDiffType.Master => 3,
            MusicDisplayItemView.DisplayDiffType.ReMaster => 4,
            MusicDisplayItemView.DisplayDiffType.Utage => 0,
            _ => throw new ArgumentOutOfRangeException()
        };
        CurrentNote = SelectedItem?.Data?.NotesData?.Notes?.ElementAtOrDefault(noteIdx);
    }
}