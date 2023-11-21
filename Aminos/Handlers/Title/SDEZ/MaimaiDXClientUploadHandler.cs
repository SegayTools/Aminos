﻿using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXClientBookkeepHandler))]
	public class MaimaiDXClientUploadHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXClientUploadHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async Task<UpsertResponseVO> UpsertClientUpload(ClientUploadRequestVO request)
		{ 
			//todo 实现一下
			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXClientBookkeepHandler);
			response.returnCode = 1;

			return response;
		}
	}
}