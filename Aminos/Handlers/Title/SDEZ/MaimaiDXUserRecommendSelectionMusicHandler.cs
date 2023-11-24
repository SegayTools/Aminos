using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserRecommendSelectionMusicHandler))]
	public class MaimaiDXUserRecommendSelectionMusicHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserRecommendSelectionMusicHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async Task<UserRecommendSelectionMusicResponseVO> GetUserRecommendSelectionMusic(UserRecommendSelectionMusicRequestVO request)
		{
			//todo 

			var response = new UserRecommendSelectionMusicResponseVO();
			response.userRecommendSelectionMusicIdList = new int[0];
			response.userId = request.userId;
			return response;
		}
	}
}
