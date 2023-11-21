using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXClientTestmodeHandler))]
	public class MaimaiDXClientTestmodeHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXClientTestmodeHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async Task<UpsertResponseVO> UpsertClientTestmode(ClientTestmodeRequestVO request)
		{
			//todo 实现一下
			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return response;
		}
	}
}
