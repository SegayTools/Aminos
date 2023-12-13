using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Utils.MethodExtensions;
using AminosUI.Controls;
using AminosUI.Models;
using AminosUI.Services.Applications;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using AminosUI.Utils.MethodExtensions;
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

    private List<MusicDisplayItem> cacheDisplayItems;
    private Dictionary<MusicDifficultyID, CompositeUserMusicDetail[]> cachedMusicDetailRanks;

    private Dictionary<int, MapBoundMusicData[]> cacheMapBoundMusicDatas;

    private MusicData[] cacheMusicDatas;

    private Dictionary<(int, MusicDifficultyID), UserMusicDetail> cacheUserMusicDetails;

    private CancellationTokenSource currentCancelSource;

    [ObservableProperty]
    private GroupMethodType currentGroupMethodType = GroupMethodType.Genre;

    [ObservableProperty]
    private MusicItemGroup currentSelectGroup;

    [ObservableProperty]
    private SortMethodType currentSortMethodType = SortMethodType.ID;

    [ObservableProperty]
    private string currentUserRank;

    [ObservableProperty]
    private CompositeUserMusicDetail[] musicDetailRankList;

    [ObservableProperty]
    private ObservableCollection<MusicItemGroup> musicGroups = new();

    [ObservableProperty]
    private MusicDisplayItem selectedItem;

    [ObservableProperty]
    private UserDetail userDetail;

    public MusicListPageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();

        UserDetail = new UserDetail
        {
            userName = "MikiraSora",
            firstDataVersion = "3.5"
        };
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

    public SortMethodType[] SortMethodTypeEnums => Enum.GetValues<SortMethodType>();
    public GroupMethodType[] GroupMethodTypeEnums => Enum.GetValues<GroupMethodType>();

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

    private async ValueTask<UserDetail> GetUserDetail(CancellationToken cancellationToken)
    {
        var cardsResp = await cardManager.GetCards(cancellationToken);
        if (!cardsResp.isSuccess)
        {
            notification.ShowError($"获取Card数据失败:{cardsResp.message}");
            return default;
        }

        if (cardsResp.obj.Length == 0)
        {
            notification.ShowError("用户并未游玩此游戏，或者游玩的Aime卡并未绑定到此用户上");
            return default;
        }

        var userDetail = default(UserDetail);
        var userId = 0UL;
        foreach (var card in cardsResp.obj)
        {
            userId = (ulong) card.AimeId;
            userDetail = await sdezDataManager.GetUserDetail(userId, cancellationToken);
            if (userDetail is not null)
                return userDetail;
        }

        return default;
    }

    private async ValueTask RefreshMusicGroupsInternal(CancellationToken cancellationToken)
    {
        var resp = await sdezDataManager.GetAllMusicData(cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法获取曲库列表:{resp.message}");
            return;
        }

        cacheMusicDatas = resp.isSuccess ? resp.obj : new MusicData[0];

        var resp3 = await sdezDataManager.GetAllMapBoundMusicData(cancellationToken);
        if (!resp3.isSuccess)
        {
            notification.ShowError($"无法获取区域列表:{resp3.message}");
            return;
        }

        cacheMapBoundMusicDatas = (resp3.isSuccess ? resp3.obj : new MapBoundMusicData[0])
            .SelectMany(x => x.MusicDatas.Select(musicId => (musicId, x)))
            .GroupBy(x => x.musicId)
            .ToDictionary(x => x.Key, x => x.Select(x => x.x).ToArray());

        UserDetail = await GetUserDetail(cancellationToken);
        if (UserDetail is null)
            return;

        var resp2 = await sdezDataManager.GetAllUserMusicDetail(UserDetail.Id, cancellationToken);
        if (!resp2.isSuccess)
            notification.ShowError($"无法获取用户成绩:{resp.message}");

        cacheUserMusicDetails = resp2.isSuccess
            ? resp2.obj.ToDictionary(x => (x.musicId, x.level), x => x)
            : new Dictionary<(int musicId, MusicDifficultyID level), UserMusicDetail>();

        await RegroupMusicDisplayItems();

        if (SelectedItem != null)
            SelectedItem = currentSelectGroup?.Items.FirstOrDefault(x => x == SelectedItem);
    }

    partial void OnCurrentSortMethodTypeChanged(SortMethodType value)
    {
        RegroupMusicDisplayItems();
    }

    partial void OnCurrentGroupMethodTypeChanged(GroupMethodType value)
    {
        RegroupMusicDisplayItems();
    }

    [RelayCommand]
    private async Task RefetchAllMusicData()
    {
        using var disp = notification.BeginLoadingNotification("获取乐曲数据中", out var cancellationToken);
        await RefreshMusicGroupsInternal(cancellationToken);
    }

    private async ValueTask RegroupMusicDisplayItems()
    {
        MusicGroups.Clear();

        if (cacheDisplayItems is null)
        {
            var list = new List<MusicDisplayItem>();
            var nonMapBound = new MapBoundMusicData
            {
                Id = -1,
                Name = "未有区域分配"
            };
            var nonMap = new List<int>();
            foreach (var musicData in cacheMusicDatas)
                for (var i = 0; i < musicData.NotesData.Notes.Count; i++)
                {
                    var note = musicData.NotesData.Notes[i];
                    if (!note.enable)
                        continue;
                    var diffId = (MusicDifficultyID) i;

                    var userMusicDetail = cacheUserMusicDetails.TryGetValue((musicData.Id, diffId), out var u)
                        ? u
                        : default;

                    var item = new MusicDisplayItem
                    {
                        MusicData = musicData,
                        Note = note,
                        UserMusicDetail = userMusicDetail,
                        DifficultyId = diffId switch
                        {
                            MusicDifficultyID.Basic => MusicDisplayItemView.DisplayDiffType.Basic,
                            MusicDifficultyID.Expert => MusicDisplayItemView.DisplayDiffType.Expert,
                            MusicDifficultyID.Advanced => MusicDisplayItemView.DisplayDiffType.Advanced,
                            MusicDifficultyID.Master => MusicDisplayItemView.DisplayDiffType.Master,
                            MusicDifficultyID.ReMaster => MusicDisplayItemView.DisplayDiffType.ReMaster,
                            MusicDifficultyID.Strong => MusicDisplayItemView.DisplayDiffType.Utage
                        }
                    };
                    if (musicData.isUtage)
                        item.DifficultyId = MusicDisplayItemView.DisplayDiffType.Utage;
                    if (!cacheMapBoundMusicDatas.TryGetValue(item.MusicData.Id, out var mb))
                    {
                        nonMap.Add(item.MusicData.Id);
                        item.MapBoundMusicData = new[] {nonMapBound};
                    }
                    else
                    {
                        item.MapBoundMusicData = mb;
                    }

                    list.Add(item);
                }

            nonMapBound.MusicDatas = nonMap.ToArray();
            cacheDisplayItems = list;
        }

        const int BpmSplitInterval = 25;

        var groups = default(Dictionary<string, List<MusicDisplayItem>>);
        if (CurrentGroupMethodType == GroupMethodType.MapBounds)
            groups = cacheDisplayItems.SelectMany(item => item.MapBoundMusicData.Select(bound => (bound, item)))
                .GroupBy(x => x.bound.Name).ToDictionary(
                    x => x.Key.Replace("ボーナス曲", string.Empty), x => x.Select(x => x.item).ToList());
        else
            groups = (CurrentGroupMethodType switch
            {
                GroupMethodType.Genre => cacheDisplayItems.GroupBy(x => x.MusicData.GenreName),
                GroupMethodType.All => cacheDisplayItems.GroupBy(x => "全部"),
                GroupMethodType.FumenType => cacheDisplayItems.GroupBy(x =>
                {
                    var str = "";
                    if (x.MusicData.isUtage)
                        str += "宴谱";
                    if (x.MusicData.IsDeluxe)
                        str += " DX";
                    if (string.IsNullOrWhiteSpace(str))
                        str = "普通";
                    return str;
                }),
                GroupMethodType.UtageType => cacheDisplayItems
                    .Where(x => !string.IsNullOrWhiteSpace(x.MusicData.UtageKanjiName))
                    .GroupBy(x => "宴谱 " + x.MusicData.UtageKanjiName),
                GroupMethodType.Rank => cacheDisplayItems.GroupBy(x =>
                    x.UserMusicDetail is UserMusicDetail d ? d.scoreRank.ToString() : "从未玩过"),
                GroupMethodType.SyncStatus => cacheDisplayItems.GroupBy(x =>
                    x.UserMusicDetail is UserMusicDetail d ? d.syncStatus.ToString() : "从未玩过"),
                GroupMethodType.ComboStatus => cacheDisplayItems.GroupBy(x =>
                    x.UserMusicDetail is UserMusicDetail d ? d.comboStatus.ToString() : "从未玩过"),
                GroupMethodType.DiffId => cacheDisplayItems.GroupBy(x => x.DifficultyId.ToString()),
                GroupMethodType.Version => cacheDisplayItems.GroupBy(x => x.MusicData.AddVersion),
                GroupMethodType.Level => cacheDisplayItems.GroupBy(x => "Lv " + x.Note.LevelDisplay),
                GroupMethodType.Name => cacheDisplayItems.GroupBy(x => x.MusicData.Name[0] + " 行"),
                GroupMethodType.Bpm => cacheDisplayItems.GroupBy(x =>
                {
                    var i = (int) (x.MusicData.Bpm / BpmSplitInterval);
                    return $"BPM {i * BpmSplitInterval} ~ {(i + 1) * BpmSplitInterval}";
                }),
                _ => throw new ArgumentOutOfRangeException()
            }).ToDictionary(x => x.Key, x => x.ToList());

        foreach (var list in groups.Values)
            switch (CurrentSortMethodType)
            {
                case SortMethodType.Artist:
                    list.SortBy(x => x.MusicData.Artist ?? string.Empty, x => x.DifficultyId);
                    break;
                case SortMethodType.DxScore:
                    list.SortBy(x => x.UserMusicDetail?.deluxscoreMax ?? default, x => x.DifficultyId);
                    break;
                case SortMethodType.Achievement:
                    list.SortBy(x => x.UserMusicDetail?.achievement ?? default, x => x.DifficultyId);
                    break;
                case SortMethodType.ID:
                    list.SortBy(x => x.MusicData.Id, x => x.DifficultyId);
                    break;
                case SortMethodType.Name:
                    list.SortBy(x => x.MusicData.Name, x => x.DifficultyId);
                    break;
                case SortMethodType.Rank:
                    list.SortBy(x => (int) (x.UserMusicDetail?.scoreRank ?? default), x => x.DifficultyId);
                    break;
                case SortMethodType.Sync:
                    list.SortBy(x => (int) (x.UserMusicDetail?.syncStatus ?? default), x => x.DifficultyId);
                    break;
                case SortMethodType.ApFc:
                    list.SortBy(x => (int) (x.UserMusicDetail?.comboStatus ?? default), x => x.DifficultyId);
                    break;
                case SortMethodType.BPM:
                    list.SortBy(x => x.MusicData.Bpm, x => x.DifficultyId);
                    break;
                case SortMethodType.Level:
                    list.SortBy(x => (int) (x.UserMusicDetail?.level ?? default), x => x.DifficultyId);
                    break;
            }

        MusicGroups.AddRange(groups.Select(x => new MusicItemGroup
        {
            Id = 0,
            Name = $"{x.Key}   ({x.Value.Count})",
            Items = x.Value
        }));
    }

    async partial void OnSelectedItemChanged(MusicDisplayItem oldValue, MusicDisplayItem newValue)
    {
        currentCancelSource?.Cancel();
        currentCancelSource = new CancellationTokenSource();
        using var disp = notification.BeginLoadingNotification("获取排行榜列表", out var cancellationToken);
        var actualCancel =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, currentCancelSource.Token);

        await RefreshUserRank(newValue, actualCancel.Token);
        await UpdateRank(newValue, cancellationToken);
    }

    [RelayCommand]
    private async Task MusicDisplayItemTapped(MusicDisplayItem item)
    {
        SelectedItem = item;
    }

    private async ValueTask UpdateRank(MusicDisplayItem item, CancellationToken cancellationToken)
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
                if (array[mid].UserMusicDetail.deluxscoreMax > target.deluxscoreMax)
                    left = mid + 1; // 目标值在右半部分
                else
                    right = mid - 1; // 目标值在左半部分
            }

            return -1; // 没有找到目标值
        }

        MusicDetailRankList =
            cachedMusicDetailRanks?.TryGetValue((MusicDifficultyID) (int) item.DifficultyId, out var arr) ?? false
                ? arr
                : new CompositeUserMusicDetail[0];
        if (SelectedItem?.UserMusicDetail is null)
        {
            CurrentUserRank = "?";
        }
        else
        {
            var idx = BinarySearchIndex(MusicDetailRankList, SelectedItem?.UserMusicDetail);
            CurrentUserRank = idx == -1 ? "?" : idx.ToString();
        }
    }

    private async Task RefreshUserRank(MusicDisplayItem newValue, CancellationToken cancellationToken)
    {
        var musicId = newValue.MusicData.Id;

        var resp = await sdezDataManager.GetMusicDetailRank(20, (MusicDifficultyID) (int) newValue.DifficultyId, 0,
            musicId,
            cancellationToken);
        if (resp.isSuccess)
        {
            cachedMusicDetailRanks = resp.obj.GroupBy(x => x.UserMusicDetail.level)
                .ToDictionary(x => x.Key, x => x.OrderByDescending(x => x.UserMusicDetail.deluxscoreMax).ToArray());

            foreach (var list in cachedMusicDetailRanks.Values)
                for (var i = 0; i < list.Length; i++)
                    list[i].Rank = i;
        }
        else
        {
            notification.ShowError($"无法加载排行榜数据:{resp.message}");
        }
    }
}