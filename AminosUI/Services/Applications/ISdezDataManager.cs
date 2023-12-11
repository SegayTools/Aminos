using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;

namespace AminosUI.Services.Applications;

public interface ISdezDataManager
{
    ValueTask<UserDetail> GetUserDetail(ulong userId, CancellationToken cancellationToken);
    ValueTask<UserOption> GetUserOption(ulong userId, CancellationToken cancellationToken);
    ValueTask<UserExtend> GetUserExtend(ulong userId, CancellationToken cancellationToken);
    ValueTask<UserDetail[]> GetUserRivals(ulong userId, CancellationToken cancellationToken);
    ValueTask<CommonApiResponse> AddRival(ulong userId, ulong rivalUserId, CancellationToken cancellationToken);
    ValueTask<CommonApiResponse> DeleteRival(ulong userId, ulong rivalUserId, CancellationToken cancellationToken);
    ValueTask<CommonApiResponse<MusicData[]>> GetAllMusicData(CancellationToken cancellationToken);

    ValueTask<GenerateCalculatedRatingResponse> GetCalculatedRatingResponse(ulong userId,
        CancellationToken cancellationToken);

    ValueTask<CommonApiResponse<UserMusicDetail[]>> GetUserMusicDetail(ulong userId, int musicId,
        CancellationToken cancellationToken);

    ValueTask<CommonApiResponse<CompositeUserMusicDetail[]>> GetMusicDetailRank(int takeCount, MusicDifficultyID level,
        int skipCount,
        int musicId,
        CancellationToken cancellationToken);
}