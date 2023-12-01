using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXClientSettingHandler))]
	public class MaimaiDXClientSettingHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXClientSettingHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UpsertResponseVO> UpsertClientSetting(ClientSettingRequestVO request)
		{
			var setting = request.clientSetting;
			maimaiDxDB.Update(setting);

			await maimaiDxDB.SaveChangesAsync();

			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return response;
		}
	}
}
