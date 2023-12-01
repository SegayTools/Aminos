using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserPreviewHandler))]
	public class MaimaiDXUserPreviewHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserPreviewHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserPreviewResponseVO> GetUserPreview(UserPreviewRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserPreviewResponseVO()
			{
				userId = request.userId,
				dispRate = (int)userDetail.UserOption.dispRate,
				dailyBonusDate = userDetail.dailyBonusDate,
				lastDataVersion = userDetail.lastDataVersion,
				lastLoginDate = userDetail.lastLoginDate,
				lastPlayDate = userDetail.lastPlayDate,
				banState = userDetail.banState,
				frameId = userDetail.frameId,
				headPhoneVolume = (int)userDetail.UserOption.headPhoneVolume,
				iconId = userDetail.iconId,
				isInherit = false,
				isLogin = false,
				trophyId = 0,
				isNetMember = userDetail.isNetMember,
				lastGameId = userDetail.lastGameId,
				lastRomVersion = userDetail.lastRomVersion,
				nameplateId = userDetail.plateId,
				partnerId = userDetail.partnerId,
				playerRating = userDetail.playerRating,
				totalAwake = userDetail.totalAwake,
				userName = userDetail.userName,
			};


			return response;
		}
	}
}
