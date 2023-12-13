using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Utils.MethodExtensions;
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

public partial class CollectionLookupPageViewModel : PageViewModelBase
{
    private readonly ICardManager cardManager;
    private readonly IApplicationHttpFactory httpFactory;
    private readonly IApplicationNavigation navigation;
    private readonly IApplicationNotification notification;
    private readonly ISdezDataManager sdezDataManager;
    private readonly IUserManager userManager;
    private List<DisplayCollectionItem> cacheItems;

    [ObservableProperty]
    private ObservableCollection<DisplayCollectionGroup> collectionGroups = new();

    [ObservableProperty]
    private DisplayCollectionItem currentFrameItem;

    [ObservableProperty]
    private DisplayCollectionItem currentIconItem;

    [ObservableProperty]
    private ItemKind currentListingItemKind;

    [ObservableProperty]
    private DisplayCollectionItem currentPartnerItem;

    [ObservableProperty]
    private DisplayCollectionItem currentPlateItem;

    [ObservableProperty]
    private DisplayCollectionGroup currentSelectGroup;

    [ObservableProperty]
    private DisplayCollectionItem currentTitleItem;

    private Regex filterRegex;

    [ObservableProperty]
    private bool hideNotGetItems;

    [ObservableProperty]
    private string regexFilterExpression;

    private UserDetail userDetail;

    public CollectionLookupPageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();
    }

    public CollectionLookupPageViewModel(IApplicationHttpFactory httpFactory,
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

    public ItemKind[] ListableItemKinds { get; } =
    {
        ItemKind.Frame,
        ItemKind.Title,
        ItemKind.Icon,
        ItemKind.Plate
    };

    public override string Title { get; } = "收藏品浏览器";

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task ApplyFilterExpression(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(RegexFilterExpression))
            filterRegex = default;
        else
            try
            {
                filterRegex = new Regex(RegexFilterExpression);
            }
            catch (Exception e)
            {
                notification.ShowError($"无法应用正则表达式:{e.Message}");
                filterRegex = null;

                RegexFilterExpression = default;
            }

        await RefreshCollectionGroupsInternal(cancellationToken);
    }

    private async void RefreshPage()
    {
        if (userManager.CurrentUser is null)
        {
            navigation.NavigatePageAsync<UserLoginPageViewModel>();
            notification.ShowWarnning("请先登录");
            return;
        }

        using var disp = notification.BeginLoadingNotification("获取数据中", out var cancellationToken);

        //todo 先当作只有一个账号来设计
        var userDetail = await GetUserDetail(cancellationToken);
        if (userDetail is null)
            return;
        this.userDetail = userDetail;

        await RefreshCollectionGroupsInternal(cancellationToken);
        ResetToUserUsed();
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

    private async ValueTask RefreshCollectionGroupsInternal(CancellationToken cancellationToken)
    {
        if (cacheItems is null)
        {
            var resp = await sdezDataManager.GetAllCollectionData(cancellationToken);
            if (!resp.isSuccess)
            {
                notification.ShowError($"无法获取收藏品列表:{resp.message}");
                return;
            }

            var resp2 = await sdezDataManager.GetAllUserItems(userDetail.Id, cancellationToken);
            if (!resp2.isSuccess)
                notification.ShowError($"无法获取用户所持收藏品列表:{resp2.message}");

            var hash = resp2.obj.Select(x => (x.itemKind, x.itemId)).ToHashSet();

            cacheItems = resp.obj.FrameDatas.Select(x => new DisplayCollectionItem
            {
                Description = x.NormText,
                Id = x.Id,
                Name = x.Name,
                Type = ItemKind.Frame,
                UrlTemplate = "/authAssets/SDEZ/assets/assetbundle/frame/UI_Frame_{6}.png",
                Genre = x.Genre
            }).ToList();
            cacheItems.AddRange(resp.obj.IconDatas.Select(x => new DisplayCollectionItem
            {
                Description = x.NormText,
                Id = x.Id,
                Name = x.Name,
                UrlTemplate = "/authAssets/SDEZ/assets/assetbundle/icon/UI_Icon_{6}.png",
                Type = ItemKind.Icon,
                Genre = x.Genre
            }));
            cacheItems.AddRange(resp.obj.PlateDatas.Select(x => new DisplayCollectionItem
            {
                Description = x.NormText,
                Id = x.Id,
                Name = x.Name,
                UrlTemplate = "/authAssets/SDEZ/assets/assetbundle/nameplate/UI_Plate_{6}.png",
                Type = ItemKind.Plate,
                Genre = x.Genre
            }));
            cacheItems.AddRange(resp.obj.TitleDatas.Select(x => new DisplayCollectionItem
            {
                Description = x.NormText,
                Id = x.Id,
                Name = x.Name,
                UrlTemplate = x.RareType,
                Type = ItemKind.Title,
                Genre = x.Genre
            }));

            await Parallel.ForEachAsync(cacheItems,
                (item, cancellationToken) =>
                {
                    item.IsEnable = hash.Contains((item.Type, item.Id));
                    return ValueTask.CompletedTask;
                }
            );
        }

        var prevName = CurrentSelectGroup?.Name;
        CollectionGroups.Clear();

        var groups = cacheItems.GroupBy(x => x.Type).Select(x => new DisplayCollectionGroup
        {
            Name = x.Key switch
            {
                ItemKind.Frame => "背景框",
                ItemKind.Icon => "头像",
                ItemKind.Plate => "姓名框",
                ItemKind.Title => "称号",
                ItemKind.Partner => "搭档"
            },
            Items = x.Where(x => HideNotGetItems ? x.IsEnable : true)
                .Where(x => filterRegex?.IsMatch(x.Name + x.Description + x.Genre + x.Id) ?? true).ToArray()
        });

        CollectionGroups.AddRange(groups);
        CurrentSelectGroup = CollectionGroups.FirstOrDefault(x => x.Name == prevName);
    }

    partial void OnHideNotGetItemsChanged(bool value)
    {
        using var disp = notification.BeginLoadingNotification("重新排列中", out var cancellationToken);
        RefreshCollectionGroupsInternal(cancellationToken);
    }

    [RelayCommand]
    private void ApplyItem(DisplayCollectionItem item)
    {
        switch (item.Type)
        {
            case ItemKind.Icon:
                CurrentIconItem = item;
                break;
            case ItemKind.Frame:
                CurrentFrameItem = item;
                break;
            case ItemKind.Plate:
                CurrentPlateItem = item;
                break;
            case ItemKind.Title:
                CurrentTitleItem = item;
                break;
        }
    }

    [RelayCommand]
    private void ResetToUserUsed()
    {
        CurrentFrameItem = cacheItems.FirstOrDefault(x => x.Id == userDetail.frameId && x.Type == ItemKind.Frame);
        CurrentTitleItem = cacheItems.FirstOrDefault(x => x.Id == userDetail.titleId && x.Type == ItemKind.Title);
        CurrentPlateItem = cacheItems.FirstOrDefault(x => x.Id == userDetail.plateId && x.Type == ItemKind.Plate);
        CurrentIconItem = cacheItems.FirstOrDefault(x => x.Id == userDetail.iconId && x.Type == ItemKind.Icon);
    }

    [RelayCommand]
    private async Task SaveApplied()
    {
        using var disp = notification.BeginLoadingNotification("保存设置中", out var cancellationToken);

        if (CurrentIconItem is null)
        {
            notification.ShowError("无法保存头像，因为还没选择数据");
            return;
        }

        var resp = await sdezDataManager.SaveUserCollectionUsing(userDetail.Id, ItemKind.Icon, CurrentIconItem.Id,
            cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法保存头像:{resp.message}");
            return;
        }

        if (CurrentPlateItem is null)
        {
            notification.ShowError("无法保存姓名框，因为还没选择数据");
            return;
        }

        resp = await sdezDataManager.SaveUserCollectionUsing(userDetail.Id, ItemKind.Plate, CurrentPlateItem.Id,
            cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法保存姓名框:{resp.message}");
            return;
        }

        if (CurrentFrameItem is null)
        {
            notification.ShowError("无法保存背景框，因为还没选择数据");
            return;
        }

        resp = await sdezDataManager.SaveUserCollectionUsing(userDetail.Id, ItemKind.Frame, CurrentFrameItem.Id,
            cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法保存背景框:{resp.message}");
            return;
        }

        if (CurrentTitleItem is null)
        {
            notification.ShowError("无法保存称号，因为还没选择数据");
            return;
        }

        resp = await sdezDataManager.SaveUserCollectionUsing(userDetail.Id, ItemKind.Title, CurrentTitleItem.Id,
            cancellationToken);
        if (!resp.isSuccess)
        {
            notification.ShowError($"无法保存称号:{resp.message}");
            return;
        }

        notification.ShowInfomation("保存成功!");
    }
}