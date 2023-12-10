using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;

namespace AminosUI.Services.Applications;

public interface IKeychipManager
{
    ValueTask<CommonApiResponse<Keychip[]>> GetKeychips(CancellationToken cancellationToken = default);
    ValueTask<CommonApiResponse> GenerateNewKeychip(string keychip, CancellationToken cancellationToken = default);
    ValueTask<CommonApiResponse> RemoveKeychip(Keychip keychip, CancellationToken cancellationToken = default);
    ValueTask<CommonApiResponse> UpdateKeychip(Keychip keychip, CancellationToken cancellationToken = default);
}