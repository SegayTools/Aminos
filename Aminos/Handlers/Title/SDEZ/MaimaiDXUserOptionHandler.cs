using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserOptionHandler))]
	public class MaimaiDXUserOptionHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserOptionHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserOptionResponseVO> GetUserOption(UserOptionRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserOptionResponseVO();
			response.userId = request.userId;
			response.userOption = userDetail.UserOption;

			return response;
		}
	}
}
