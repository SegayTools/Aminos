using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils.MethodExtensions;

namespace AminosUI.Services.Applications.DefaultImpl;

[RegisterInjectable(typeof(IKeychipManager))]
public class DefaultKeychipManager : IKeychipManager
{
    private readonly IApplicationHttpFactory httpFactory;

    public DefaultKeychipManager(IApplicationHttpFactory httpFactory)
    {
        this.httpFactory = httpFactory;
    }

    public async ValueTask<CommonApiResponse> RemoveKeychip(Keychip keychip,
        CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.PostAsCommonApi("api/Keychip/Delete", new
        {
            keychipId = keychip.Id
        }, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse> UpdateKeychip(Keychip keychip,
        CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.PostAsCommonApi("api/Keychip/Update", new
        {
            keychipId = keychip.Id,
            newName = keychip.Name,
            newEnable = keychip.Enable
        }, cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse<Keychip[]>> GetKeychips(CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.GetAsCommonApi<Keychip[]>("api/Keychip/GetKeychips", default,
            cancellationToken);
        return resp;
    }

    public async ValueTask<CommonApiResponse> GenerateNewKeychip(string keychip,
        CancellationToken cancellationToken = default)
    {
        var resp = await httpFactory.PostAsCommonApi<Keychip>("api/Keychip/Create", new
            {
                keychipId = keychip
            },
            cancellationToken);
        return resp;
    }
}