using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserCardHandler))]
	public class MaimaiDXUserCardHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserCardHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async Task<UserCardResponseVO> GetUserCard(UserCardRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.Include(x => x.UserCards)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserCardResponseVO();
			response.userCardList = userDetail.UserCards
				.Skip(request.nextIndex).Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userCardList.Length;
			response.userId = request.userId;

			return response;
		}
	}
}
