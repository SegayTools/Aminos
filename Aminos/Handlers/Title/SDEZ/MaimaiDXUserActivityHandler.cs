using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXUserActivityHandler))]
public class MaimaiDXUserActivityHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXUserActivityHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async Task<UserActivityResponseVO> GetUserActivity(UserActivityRequestVO request)
    {
        var userDetail = await maimaiDxDB.UserDetails
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        var response = new UserActivityResponseVO();
        response.userActivity = new UserActivityResponseVO.UserActivityWrap(userDetail.UserActivities);

        return response;
    }
}