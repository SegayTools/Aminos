using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserLoginHandler))]
	public class MaimaiDXUserLoginHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserLoginHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserLoginResponseVO> UserLogin(UserLoginRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserLoginResponseVO()
			{
				returnCode = 1,
				lastLoginDate = userDetail?.lastLoginDate ?? string.Empty,
				Bearer = string.Empty,
				consecutiveLoginCount = 0,
				loginCount = 1,
				loginId = 1
			};

			return response;
		}

		public async ValueTask<UserLogoutResponseVO> UserLogout(UserLogoutRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserLogoutResponseVO()
			{
				returnCode = 1,
			};

			return response;
		}
	}
}
