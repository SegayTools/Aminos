using System.Threading;
using System.Threading.Tasks;
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

    public async ValueTask<GenerateCalculatedRatingResponse> GetCalculatedRatingResponse(ulong userId,
        CancellationToken cancellationToken)
    {
        var resp = await httpFactory.GetAsCommonApi<GenerateCalculatedRatingResponse>(
            $"api/SDEZ/GetUserCalculatedRatingList?userId={userId}", default, cancellationToken);
        return resp.obj;
    }
}