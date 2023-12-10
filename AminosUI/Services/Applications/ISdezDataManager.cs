using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;

namespace AminosUI.Services.Applications;

public interface ISdezDataManager
{
    public ValueTask<UserDetail> GetUserDetail(ulong userId, CancellationToken cancellationToken);
    public ValueTask<UserOption> GetUserOption(ulong userId, CancellationToken cancellationToken);
    public ValueTask<UserExtend> GetUserExtend(ulong userId, CancellationToken cancellationToken);
    public ValueTask<GenerateCalculatedRatingResponse> GetCalculatedRatingResponse(ulong userId, CancellationToken cancellationToken);
}