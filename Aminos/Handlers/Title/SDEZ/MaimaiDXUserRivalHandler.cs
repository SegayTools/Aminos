using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserRivalHandler))]
public class MaimaiDXUserRivalHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserRivalHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async Task<UserRivalResponseVO> GetUserRivalData(UserRivalRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var rivalUserDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.rivalId);

        var response = new UserRivalResponseVO();
        response.userId = request.userId;
        response.userRivalData = new UserRivalData
        {
            rivalId = rivalUserDetail.Id,
            rivalName = rivalUserDetail.userName
        };

        return response;
    }

    public async Task<UserRivalMusicResponseVO> GetUserRivalMusic(UserRivalMusicRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var rivalUserDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.rivalId);

        var nextIndex = request.nextIndex;

        //fix maxCount and sort by musicId so that we could fetch entity music with full fumen diffs.
        var maxCount = int.MaxValue;

        var groupedMusicDetails = rivalUserDetail.UserMusicDetails
            .OrderBy(x => x.musicId)
            .Skip(nextIndex).Take(maxCount)
            .GroupBy(x => x.musicId)
            .ToArray()
            .Select(x => new UserRivalMusic
            {
                musicId = x.Key,
                userRivalMusicDetailList = x.Select(y => new UserRivalMusicDetail
                {
                    achievement = (int) y.achievement,
                    deluxscoreMax = (int) y.deluxscoreMax,
                    level = (int) y.level
                }).ToArray()
            });

        var filterMusicDetails = new List<UserRivalMusic>();

        var actualReadCount = 0;
        foreach (var musicDetail in groupedMusicDetails)
        {
            var length = musicDetail.userRivalMusicDetailList.Length;
            if (length + actualReadCount > maxCount)
                break;
            filterMusicDetails.Add(musicDetail);
            actualReadCount += length;
        }

        var respNextIndex = nextIndex + actualReadCount;
        if (filterMusicDetails.Count <= 0)
            respNextIndex = 0; //nofity client that All music details had been sent (for this rival).
        else
            filterMusicDetails.Sort((a, b) => a.musicId.CompareTo(b.musicId));

        var response = new UserRivalMusicResponseVO();

        response.rivalId = rivalUserDetail.Id;
        response.userId = userDetail.Id;
        response.nextIndex = respNextIndex;
        response.userRivalMusicList = filterMusicDetails.ToArray();

        return response;
    }

    public async Task<CommonApiResponse> GetRivalUserList(ulong userId)
    {
        var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(false, "userId无效");

        var items = userDetail.UserFavoriteItems.Where(x => x.kind == 2).Select(x => x.id).ToArray();
        var users = (await maimaiDxDB.UserDetails.Where(x => items.Contains(x.Id)).ToArrayAsync()).ToArray();

        return new CommonApiResponse<UserDetail[]>(true, users);
    }

    public async Task<CommonApiResponse> AddRival(ulong userId, ulong rivalUserId)
    {
        var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(false, "userId无效");
        var rivalUserDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == rivalUserId);
        if (rivalUserDetail is null)
            return new CommonApiResponse(false, "rivalUserId无效");

        if (!userDetail.UserFavoriteItems.Any(x => x.id == rivalUserId && x.kind == 2))
        {
            userDetail.UserFavoriteItems.Add(new UserFavoriteItem
            {
                id = rivalUserId,
                kind = 2
            });
            await maimaiDxDB.SaveChangesAsync();
        }

        return new CommonApiResponse(true);
    }

    public async Task<CommonApiResponse> DeleteRival(ulong userId, ulong rivalUserId)
    {
        var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetail is null)
            return new CommonApiResponse(false, "userId无效");
        var rivalUserDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == rivalUserId);
        if (rivalUserDetail is null)
            return new CommonApiResponse(false, "rivalUserId无效");

        var item = userDetail.UserFavoriteItems.FirstOrDefault(x => x.id == rivalUserId && x.kind == 2);
        if (item is not null)
        {
            userDetail.UserFavoriteItems.Remove(item);
            await maimaiDxDB.SaveChangesAsync();
        }

        return new CommonApiResponse(true);
    }
}