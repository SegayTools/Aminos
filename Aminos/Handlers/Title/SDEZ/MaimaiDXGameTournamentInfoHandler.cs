using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameTournamentInfoHandler))]
	public class MaimaiDXGameTournamentInfoHandler
	{
		public Task<GameTournamentInfoResponseVO> GetGameTournamentInfo(GameTournamentInfoRequestVO request)
		{
			//todo 后面再看看能不能实现

			var response = new GameTournamentInfoResponseVO();
			response.gameTournamentInfoList = new GameTournamentInfo[0];
			response.length = response.gameTournamentInfoList.LongLength;

			return Task.FromResult(response);
		}
	}
}
