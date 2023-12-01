using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXClientUploadHandler))]
	public class MaimaiDXClientUploadHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXClientUploadHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public ValueTask<UpsertResponseVO> UpsertClientUpload(ClientUploadRequestVO request)
		{
			//todo 实现一下
			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return ValueTask.FromResult(response);
		}
	}
}
