using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserLoginBonusHandler))]
	public class MaimaiDXUserLoginBonusHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserLoginBonusHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserLoginBonusResponseVO> GetUserLoginBonus(UserLoginBonusRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserLoginBonusResponseVO();
			response.userId = request.userId;
			response.userLoginBonusList = userDetail.UserLoginBonuses
				.Skip((int)request.nextIndex)
				.Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userLoginBonusList.LongLength;
			if (response.userLoginBonusList.Length == 0)
				response.nextIndex = 0;
			return response;
		}
	}
}
