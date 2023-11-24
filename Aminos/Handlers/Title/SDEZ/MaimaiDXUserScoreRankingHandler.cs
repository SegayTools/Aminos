using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserScoreRankingHandler))]
	public class MaimaiDXUserScoreRankingHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserScoreRankingHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserScoreRankingResponseVO> GetUserScoreRanking(UserScoreRankingRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserScoreRankingResponseVO();
			response.userId = request.userId;
			//todo rank排序
			response.userScoreRanking = userDetail.UserScoreRankings.FirstOrDefault(x => x.tournamentId == request.competitionId);

			return response;
		}
	}
}
