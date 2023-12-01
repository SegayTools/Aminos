﻿using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserFavoriteItemHandler))]
	public class MaimaiDXUserFavoriteItemHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserFavoriteItemHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserFavoriteItemResponseVO> GetUserFavoriteItem(UserFavoriteItemRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserFavoriteItemResponseVO();
			response.userId = request.userId;
			response.userFavoriteItemList = userDetail.UserFavoriteItems
				.Where(x => x.kind == request.kind)
				.Skip((int)request.nextIndex)
				.Take(request.maxCount).ToArray();
			response.nextIndex = (ulong)((int)request.nextIndex + response.userFavoriteItemList.Length);
			request.kind = response.kind;

			return response;
		}
	}
}
