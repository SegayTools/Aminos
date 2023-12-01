using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserMapBonusHandler))]
	public class MaimaiDXUserMapBonusHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserMapBonusHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserMapResponseVO> GetUserMap(UserMapRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserMapResponseVO();
			response.userId = request.userId;
			response.userMapList = userDetail.UserMaps
				.Skip((int)request.nextIndex)
				.Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userMapList.LongLength;
			if (response.userMapList.Length == 0)
				response.nextIndex = 0;
			return response;
		}
	}
}
