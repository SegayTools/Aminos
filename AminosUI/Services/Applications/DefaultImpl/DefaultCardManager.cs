using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils.MethodExtensions;

namespace AminosUI.Services.Applications.DefaultImpl;

[RegisterInjectable(typeof(ICardManager))]
public class DefaultCardManager : ICardManager
{
    private readonly IApplicationHttpFactory httpFactory;

    public DefaultCardManager(IApplicationHttpFactory httpFactory)
    {
        this.httpFactory = httpFactory;
    }

    public async ValueTask<CommonApiResponse<Card[]>> GetCards(CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.GetAsCommonApi<Card[]>("api/Card/GetCards", default, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse> BindCard(string accessCode, CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.PostAsCommonApi("api/Card/BindCardToUser", new {accessCode}, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse> UnbindCard(string accessCode,
        CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.PostAsCommonApi("api/Card/UnbindCardToUser", new {accessCode}, cancellationToken);
        return resp;
    }
}