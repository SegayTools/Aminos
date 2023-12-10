using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserRatingHandler))]
public class MaimaiDXUserRatingHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserRatingHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async ValueTask<UserRatingResponseVO> GetUserRating(UserRatingRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var response = new UserRatingResponseVO();
        response.userId = request.userId;
        response.userRating = userDetail.UserRating;

        return response;
    }

    public async ValueTask<CommonApiResponse> GetUserCalculatedRatingList(ulong userId)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(false, "找不到此用户");

        var ratingList = userDetail.UserRating.ratingList.Select(x => GenerateCalculatedRating(x, userDetail))
            .OfType<CalculatedRating>().ToArray();
        var nextRatingList = userDetail.UserRating.nextRatingList.Select(x => GenerateCalculatedRating(x, userDetail))
            .OfType<CalculatedRating>().ToArray();

        return new CommonApiResponse<GenerateCalculatedRatingResponse>(true, new GenerateCalculatedRatingResponse
        {
            RatingList = ratingList, NextRatingList = nextRatingList
        });
    }

    private CalculatedRating GenerateCalculatedRating(UserRate rate, UserDetail detail)
    {
        var musicId = rate.musicId;
        var musicData = maimaiDxDB.MusicDatas.FirstOrDefault(x => x.Id == musicId);
        if (musicData is null)
            return default;
        var note = musicData.NotesData.Notes.ElementAtOrDefault(rate.level);
        if (note is null)
            return default;
        var musicDetail = detail.UserMusicDetails.FirstOrDefault(x => x.musicId == musicId && x.level == x.level);
        var diff = note.Level + note.LevelDecimal / 100.0f;
        var val = UserRate.CalculateRating(diff, musicDetail.achievement);

        return new CalculatedRating
        {
            MusicDetail = musicDetail,
            RatingValue = val,
            MusicData = musicData
        };
    }
}