using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameChargeHandler))]
	public class MaimaiDXGameChargeHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXGameChargeHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<GameChargeResponseVO> GetGameCharge(GameChargeRequestVO request)
		{
			var response = new GameChargeResponseVO();
			response.gameChargeList = await maimaiDxDB.GameCharges.ToArrayAsync();
			response.length = response.gameChargeList.LongLength;

			return response;
		}
	}
}
