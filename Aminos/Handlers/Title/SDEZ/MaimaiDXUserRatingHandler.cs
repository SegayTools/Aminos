using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserRatingHandler))]
	public class MaimaiDXUserRatingHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserRatingHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserRatingResponseVO> GetUserRating(UserRatingRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserRatingResponseVO();
			response.userId = request.userId;
			response.userRating = userDetail.UserRating;

			return response;
		}
	}
}
