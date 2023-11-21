using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserDataHandler))]
	public class MaimaiDXUserDataHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserDataHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserDetailResponseVO> GetUserData(UserDetailRequestVO request)
		{
			var response = new UserDetailResponseVO();
			response.userId = request.userId;
			response.userData = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == request.userId);
			response.banState = response.userData?.banState ?? 0;

			return response;
		}
	}
}
