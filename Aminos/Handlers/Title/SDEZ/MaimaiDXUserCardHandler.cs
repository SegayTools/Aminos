using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
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
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserCardResponseVO();
			response.userCardList = userDetail.UserCards
				.Skip(request.nextIndex).Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userCardList.Length;
			if (response.userCardList.Length == 0)
				response.nextIndex = 0;
			response.userId = request.userId;

			return response;
		}
	}
}
