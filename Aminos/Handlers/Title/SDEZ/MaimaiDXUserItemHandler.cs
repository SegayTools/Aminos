using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
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
				.Include(x => x.UserItems)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserItemResponseVO();
			response.userId = request.userId;
			response.userItemList = userDetail.UserItems.Skip((int)request.nextIndex).Take(request.maxCount).ToArray();
			response.nextIndex = request.nextIndex + response.userItemList.LongLength;

			return response;
		}
	}
}
