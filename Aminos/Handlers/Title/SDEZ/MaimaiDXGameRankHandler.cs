using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameRankHandler))]
	public class MaimaiDXGameRankHandler
	{
		public Task<GameRankingResponseVO> GetGameRank(GameRankingRequestVO request)
		{
			//todo 有时间看看能不能实现吧

			var response = new GameRankingResponseVO();
			response.type = request.type;
			response.gameRankingList = new GameRanking[0];

			return Task.FromResult(response);
		}
	}
}
