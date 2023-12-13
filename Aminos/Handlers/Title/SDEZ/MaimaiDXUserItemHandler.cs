using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserItemHandler))]
public class MaimaiDXUserItemHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserItemHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async ValueTask<UserItemResponseVO> GetUserItem(UserItemRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var fixedNextIndex = request.nextIndex > int.MaxValue ? 0 : (int) request.nextIndex;

        var response = new UserItemResponseVO();
        response.userId = request.userId;
        response.userItemList = userDetail.UserItems.Skip(fixedNextIndex).Take(request.maxCount).ToArray();
        response.nextIndex = fixedNextIndex + response.userItemList.LongLength;
        if (response.userItemList.Length == 0)
            response.nextIndex = 0;

        return response;
    }

    public async ValueTask<CommonApiResponse> GetAllUserItems(ulong userId)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (userDetail is null)
            return new CommonApiResponse(false, "userId无效");

        return new CommonApiResponse<UserItem[]>(true, userDetail.UserItems.ToArray());
    }

    public async Task<CommonApiResponse> SaveUserCollectionUsing(ulong userId, ItemKind itemKind, int itemId)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(false, "userId无效");

        var item = userDetail.UserItems.FirstOrDefault(x =>
            x.itemId == itemId && x.itemKind == itemKind);
        if (item is null)
            return new CommonApiResponse(false, "item无效");

        switch (item.itemKind)
        {
            case ItemKind.Icon:
                userDetail.iconId = item.itemId;
                break;
            case ItemKind.Plate:
                userDetail.plateId = item.itemId;
                break;
            case ItemKind.Title:
                userDetail.titleId = item.itemId;
                break;
            case ItemKind.Frame:
                userDetail.frameId = item.itemId;
                break;
            default:
                return new CommonApiResponse(false, "暂时不支持{item.itemKind}设置");
        }

        await maimaiDxDB.SaveChangesAsync();
        return new CommonApiResponse(true);
    }
}