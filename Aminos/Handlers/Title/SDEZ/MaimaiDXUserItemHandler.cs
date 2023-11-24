using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserItemHandler))]
	public class MaimaiDXUserItemHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserItemHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserItemResponseVO> GetUserItem(UserItemRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var fixedNextIndex = request.nextIndex > int.MaxValue ? 0 : (int)request.nextIndex;

			var response = new UserItemResponseVO();
			response.userId = request.userId;
			response.userItemList = userDetail.UserItems.Skip(fixedNextIndex).Take(request.maxCount).ToArray();
			response.nextIndex = fixedNextIndex + response.userItemList.LongLength;
			if (response.userItemList.Length == 0)
				response.nextIndex = 0;

			return response;
		}
	}
}
