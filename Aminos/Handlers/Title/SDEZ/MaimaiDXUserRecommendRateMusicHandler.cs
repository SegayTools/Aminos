using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserRecommendRateMusicHandler))]
	public class MaimaiDXUserRecommendRateMusicHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserRecommendRateMusicHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public ValueTask<UserRecommendRateMusicResponseVO> GetUserRecommendRateMusic(UserRecommendRateMusicRequestVO request)
		{
			var response = new UserRecommendRateMusicResponseVO();
			response.userId = request.userId;
			response.userRecommendRateMusicIdList = new UserRecommendRateMusic[0];

			return ValueTask.FromResult(response);
		}
	}
}
