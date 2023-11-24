using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameEventHandler))]
	public class MaimaiDXGameEventHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXGameEventHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<GameEventResponseVO> GetGameEvent(GameEventRequestVO request)
		{
			var response = new GameEventResponseVO();
			response.type = request.type;
			response.gameEventList = await maimaiDxDB.GameEvents.Where(x => x.enable && x.type == 0).ToArrayAsync();

			return response;
		}
	}
}
