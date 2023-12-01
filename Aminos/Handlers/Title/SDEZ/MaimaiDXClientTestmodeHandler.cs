﻿using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;

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

		public async ValueTask<UpsertResponseVO> UpsertClientTestmode(ClientTestmodeRequestVO request)
		{
			var testMode = request.clientTestmode;
			maimaiDxDB.Update(testMode);

			await maimaiDxDB.SaveChangesAsync();

			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return response;
		}
	}
}
