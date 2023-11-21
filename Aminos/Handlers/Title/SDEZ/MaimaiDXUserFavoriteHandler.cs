using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
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
				.Include(
					x => x.UserFavorites)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserFavoriteResponseVO();
			response.userId = request.userId;
			response.userFavoriteData = userDetail.UserFavorites.Where(x => x.itemKind == request.itemKind).FirstOrDefault();

			return response;
		}
	}
}
