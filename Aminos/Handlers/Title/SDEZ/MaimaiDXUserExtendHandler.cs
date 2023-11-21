﻿using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserExtendHandler))]
	public class MaimaiDXUserExtendHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserExtendHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserExtendResponseVO> GetUserExtend(UserExtendRequestVO request)
		{
			var response = new UserExtendResponseVO();
			response.userExtend = (await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == request.userId)).UserExtend;
			response.userId = request.userId;

			return response;
		}
	}
}
