using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
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
				.Include(x => x.UserMaps)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserMapResponseVO();
			response.userId = request.userId;
			response.userMapList = userDetail.UserMaps
				.Skip((int)request.nextIndex)
				.Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userMapList.LongLength;

			return response;
		}
	}
}
