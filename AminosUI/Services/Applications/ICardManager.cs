using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;

namespace AminosUI.Services.Applications;

public interface ICardManager
{
    ValueTask<CommonApiResponse<Card[]>> GetCards(CancellationToken cancellationToken = default);
    ValueTask<CommonApiResponse> BindCard(string accessCode, CancellationToken cancellationToken = default);
    ValueTask<CommonApiResponse> UnbindCard(string accessCode, CancellationToken cancellationToken = default);
}