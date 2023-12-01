using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserFavoriteHandler))]
	public class MaimaiDXUserFavoriteHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserFavoriteHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserFavoriteResponseVO> GetUserFavorite(UserFavoriteRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserFavoriteResponseVO();
			response.userId = request.userId;
			response.userFavoriteData = userDetail.UserFavorites.Where(x => x.itemKind == request.itemKind).FirstOrDefault() ?? new UserFavorite();

			return response;
		}
	}
}
