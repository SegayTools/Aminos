﻿using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserChargeHandler))]
	public class MaimaiDXGameNgMusicIdHandler
	{
		public async ValueTask<GameNgMusicIdResponseVO> GetGameNgMusicId(GameNgMusicIdRequestVO request)
		{
			var response = new GameNgMusicIdResponseVO();
			response.musicIdList = new int[0];
			response.length = 0;

			return response;
		}
	}
}