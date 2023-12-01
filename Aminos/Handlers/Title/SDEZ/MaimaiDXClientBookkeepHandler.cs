using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXClientBookkeepHandler))]
	public class MaimaiDXClientBookkeepHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXClientBookkeepHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UpsertResponseVO> UpsertClientBookkeeping(ClientBookkeepingRequestVO request)
		{
			var booking = request.clientBookkeeping;
			maimaiDxDB.Update(booking);

			await maimaiDxDB.SaveChangesAsync();

			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return response;
		}
	}
}
