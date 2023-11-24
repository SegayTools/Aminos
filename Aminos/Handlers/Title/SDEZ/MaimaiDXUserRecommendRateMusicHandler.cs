using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
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
