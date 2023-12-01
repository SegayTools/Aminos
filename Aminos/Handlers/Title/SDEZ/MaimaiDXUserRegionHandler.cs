using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserRegionHandler))]
	public class MaimaiDXUserRegionHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserRegionHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserRegionResponseVO> GetUserRegion(UserRegionRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserRegionResponseVO();
			response.userId = request.userId;
			response.userRegionList = userDetail.UserRegions.ToArray();
			response.length = (ulong)response.userRegionList.Length;

			return response;
		}
	}
}
