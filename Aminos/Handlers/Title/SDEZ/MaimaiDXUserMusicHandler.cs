using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserMusicHandler))]
public class MaimaiDXUserMusicHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserMusicHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async ValueTask<UserMusicResponseVO> GetUserMusic(UserMusicRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var musicDetails = userDetail.UserMusicDetails.Skip(request.nextIndex).Take(request.maxCount).ToArray();

        var response = new UserMusicResponseVO();
        response.userId = request.userId;
        response.userMusicList = musicDetails.Length > 0
            ? new UserMusic[] {new() {userMusicDetailList = musicDetails}}
            : new UserMusic[0];
        response.nextIndex = request.nextIndex + musicDetails.Length;
        if (musicDetails.Length == 0)
            response.nextIndex = 0;
        return response;
    }

    public async ValueTask<CommonApiResponse> GetUserMusicDetail(ulong userId, int musicId)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(true, "userId无效");
        var musicDetails = userDetail.UserMusicDetails.Where(x => x.musicId == musicId).ToArray();

        return new CommonApiResponse<UserMusicDetail[]>(true, musicDetails);
    }

    public async ValueTask<CommonApiResponse> GetMusicDetailRank(int musicId, int takeCount)
    {
        var details = await maimaiDxDB.UserMusicDetails.Where(x => x.musicId == musicId)
            .OrderBy(x => x.deluxscoreMax).Take(takeCount).ToArrayAsync();

        return new CommonApiResponse<CompositeUserMusicDetail[]>(true, details.Select(x => new CompositeUserMusicDetail
        {
            UserMusicDetail = x,
            UserDetail = x.UserDetail
        }).ToArray());
    }
}