using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils.MethodExtensions;

namespace AminosUI.Services.Applications.DefaultImpl;

[RegisterInjectable(typeof(ISdezDataManager))]
public class DefaultSdezDataManager : ISdezDataManager
{
    private readonly IApplicationHttpFactory httpFactory;

    public DefaultSdezDataManager(IApplicationHttpFactory httpFactory)
    {
        this.httpFactory = httpFactory;
    }

    public async ValueTask<UserDetail> GetUserDetail(ulong userId, CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetJson<UserDetailResponseVO>("api/SDEZ/GetUserDetail", new UserDetailRequestVO
        {
            userId = userId
        }, cancellationToken);
        return resp?.userData;
    }

    public async ValueTask<UserOption> GetUserOption(ulong userId, CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetJson<UserOptionResponseVO>("api/SDEZ/GetUserOption", new UserOptionRequestVO
        {
            userId = userId
        }, cancellationToken);
        return resp?.userOption;
    }

    public async ValueTask<UserExtend> GetUserExtend(ulong userId, CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetJson<UserExtendResponseVO>("api/SDEZ/GetUserExtend", new UserExtendRequestVO
        {
            userId = userId
        }, cancellationToken);
        return resp?.userExtend;
    }

    public async ValueTask<UserDetail[]> GetUserRivals(ulong userId, CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<UserDetail[]>(
            $"api/SDEZ/GetRivalUserList?userId={userId}", default, cancellationToken);
        return resp.obj;
    }

    public async ValueTask<CommonApiResponse> AddRival(ulong userId, ulong rivalUserId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.PostAsCommonApi("api/SDEZ/AddRival", new
        {
            userId, rivalUserId
        }, cancellationToken);

        return resp;
    }

    public async ValueTask<CommonApiResponse> DeleteRival(ulong userId, ulong rivalUserId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.PostAsCommonApi("api/SDEZ/DeleteRival", new
        {
            userId, rivalUserId
        }, cancellationToken);

        return resp;
    }

    public async ValueTask<CommonApiResponse<MusicData[]>> GetAllMusicData(CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<MusicData[]>("api/SDEZ/GetAllMusicData",
            default, cancellationToken);

        return resp;
    }

    public async ValueTask<GenerateCalculatedRatingResponse> GetCalculatedRatingResponse(ulong userId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<GenerateCalculatedRatingResponse>(
            $"api/SDEZ/GetUserCalculatedRatingList?userId={userId}", default, cancellationToken);
        return resp.obj;
    }

    public async ValueTask<CommonApiResponse<UserMusicDetail[]>> GetAllUserMusicDetail(ulong userId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<UserMusicDetail[]>(
            "api/SDEZ/GetAllUserMusicDetail", new
            {
                userId
            }, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse<CompositeUserMusicDetail[]>> GetMusicDetailRank(int takeCount,
        MusicDifficultyID level, int skipCount, int musicId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<CompositeUserMusicDetail[]>(
            "api/SDEZ/GetMusicDetailRank", new
            {
                takeCount,
                level,
                skipCount,
                musicId
            }, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse<MapBoundMusicData[]>> GetAllMapBoundMusicData(
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<MapBoundMusicData[]>(
            "api/SDEZ/GetAllMapBoundMusicData", default, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse<AllCollectionData>> GetAllCollectionData(
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<AllCollectionData>(
            "api/SDEZ/GetAllCollectionData", default, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse<UserItem[]>> GetAllUserItems(ulong userId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<UserItem[]>(
            "api/SDEZ/GetAllUserItems", new
            {
                userId
            }, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse> SaveUserCollectionUsing(ulong userId, ItemKind itemKind, int itemId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.PostAsCommonApi(
            "api/SDEZ/SaveUserCollectionUsing", new
            {
                userId,
                itemId,
                itemKind
            }, cancellationToken);
        return resp;
    }
}