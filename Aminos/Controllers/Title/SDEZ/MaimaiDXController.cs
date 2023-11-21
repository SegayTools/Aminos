using Aminos.Handlers.Title.SDEZ;
using Aminos.Models.Title.SDEZ.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.Title.SDEZ
{
	[ApiController]
	[Route("{userHash}/SDEZ/{version}/Maimai2Servlet")]
	[TitleZlibCompression]
	public class MaimaiDXController : TitleControllerBase
	{
		private readonly ILogger<MaimaiDXController> logger;

		public MaimaiDXController(ILogger<MaimaiDXController> logger)
		{
			this.logger = logger;
		}

		[HttpPost("GetGameEventApi")]
		public async ValueTask<IActionResult> GetGameEventApi(GameEventRequestVO request, MaimaiDXGameEventHandler handler)
		{
			//todo 塞event数据进数据库
			var response = await handler.GetGameEvent(request);
			return Json(response);
		}

		[HttpPost("GetGameRankingApi")]
		public async ValueTask<IActionResult> GetGameRankingApi(GameRankingRequestVO request, MaimaiDXGameRankHandler handler)
		{
			var response = await handler.GetGameRank(request);
			return Json(response);
		}

		[HttpPost("GetGameSettingApi")]
		public async ValueTask<IActionResult> GetGameSettingApi(GameSettingRequestVO request, MaimaiDXGameSettingHandler handler)
		{
			var response = await handler.GetGameSetting(request);
			return Json(response);
		}

		[HttpPost("GetGameTournamentInfoApi")]
		public async ValueTask<IActionResult> GetGameTournamentInfoApi(GameTournamentInfoRequestVO request, MaimaiDXGameTournamentInfoHandler handler)
		{
			var response = await handler.GetGameTournamentInfo(request);
			return Json(response);
		}

		[HttpPost("GetTransferFriendApi")]
		public async ValueTask<IActionResult> GetTransferFriendApi(TransferFriendRequestVO request, MaimaiDXTransferFriendHandler handler)
		{
			var response = await handler.GetTransferFriend(request);
			return Json(response);
		}

		[HttpPost("GetUserActivityApi")]
		public async ValueTask<IActionResult> GetUserActivityApi(UserActivityRequestVO request, MaimaiDXUserActivityHandler handler)
		{
			var response = await handler.GetUserActivity(request);
			return Json(response);
		}

		[HttpPost("GetUserCardApi")]
		public async ValueTask<IActionResult> GetUserCardApi(UserCardRequestVO request, MaimaiDXUserCardHandler handler)
		{
			var response = await handler.GetUserCard(request);
			return Json(response);
		}

		[HttpPost("GetUserCharacterApi")]
		public async ValueTask<IActionResult> GetUserCharacterApi(UserCharacterRequestVO request, MaimaiDXUserCharacterHandler handler)
		{
			var response = await handler.GetUserCharacter(request);
			return Json(response);
		}

		[HttpPost("GetUserDataApi")]
		public async ValueTask<IActionResult> GetUserDataApi(UserDetailRequestVO request, MaimaiDXUserDataHandler handler)
		{
			var response = await handler.GetUserData(request);
			return Json(response);
		}

		[HttpPost("GetUserExtendApi")]
		public async ValueTask<IActionResult> GetUserExtendApi(UserExtendRequestVO request, MaimaiDXUserExtendHandler handler)
		{
			var response = await handler.GetUserExtend(request);
			return Json(response);
		}

		[HttpPost("GetUserFavoriteApi")]
		public async ValueTask<IActionResult> GetUserFavoriteApi(UserFavoriteRequestVO request, MaimaiDXUserFavoriteHandler handler)
		{
			var response = await handler.GetUserFavorite(request);
			return Json(response);
		}

		[HttpPost("GetUserFavoriteItemApi")]
		public async ValueTask<IActionResult> GetUserFavoriteItemApi(UserFavoriteItemRequestVO request, MaimaiDXUserFavoriteItemHandler handler)
		{
			var response = await handler.GetUserFavoriteItem(request);
			return Json(response);
		}

		[HttpPost("GetUserGhostApi")]
		public async ValueTask<IActionResult> GetUserGhostApi(UserGhostRequestVO request, MaimaiDXUserGhostHandler handler)
		{
			var response = await handler.GetUserGhost(request);
			return Json(response);
		}

		[HttpPost("GetUserItemApi")]
		public async ValueTask<IActionResult> GetUserItemApi(UserItemRequestVO request, MaimaiDXUserItemHandler handler)
		{
			var response = await handler.GetUserItem(request);
			return Json(response);
		}

		[HttpPost("GetUserLoginBonusApi")]
		public async ValueTask<IActionResult> GetUserLoginBonusApi(UserLoginBonusRequestVO request, MaimaiDXUserLoginBonusHandler handler)
		{
			var response = await handler.GetUserLoginBonus(request);
			return Json(response);
		}

		[HttpPost("GetUserMapApi")]
		public async ValueTask<IActionResult> GetUserMapApi(UserMapRequestVO request, MaimaiDXUserMapBonusHandler handler)
		{
			var response = await handler.GetUserMap(request);
			return Json(response);
		}

		[HttpPost("GetUserMusicApi")]
		public async ValueTask<IActionResult> GetUserMusicApi(UserMusicRequestVO request, MaimaiDXUserMusicHandler handler)
		{
			var response = await handler.GetUserMusic(request);
			return Json(response);
		}

		[HttpPost("GetUserOptionApi")]
		public async ValueTask<IActionResult> GetUserOptionApi(UserOptionRequestVO request, MaimaiDXUserOptionHandler handler)
		{
			var response = await handler.GetUserOption(request);
			return Json(response);
		}

		[HttpPost("GetUserPortraitApi")]
		public async ValueTask<IActionResult> GetUserPortraitApi(GetUserPortraitRequestVO request, MaimaiDXUserPortraitHandler handler)
		{
			var response = await handler.GetUserPortrait(request);
			return Json(response);
		}

		[HttpPost("GetUserPreviewApi")]
		public async ValueTask<IActionResult> GetUserPreviewApi(UserPreviewRequestVO request, MaimaiDXUserPreviewHandler handler)
		{
			var response = await handler.GetUserPreview(request);
			return Json(response);
		}

		[HttpPost("GetUserRatingApi")]
		public async ValueTask<IActionResult> GetUserRatingApi(UserRatingRequestVO request, MaimaiDXUserRatingHandler handler)
		{
			var response = await handler.GetUserRating(request);
			return Json(response);
		}

		[HttpPost("GetUserRegionApi")]
		public async ValueTask<IActionResult> GetUserRegionApi(UserRegionRequestVO request, MaimaiDXUserRegionHandler handler)
		{
			var response = await handler.GetUserRegion(request);
			return Json(response);
		}

		[HttpPost("GetUserScoreRankingApi")]
		public async ValueTask<IActionResult> GetUserScoreRankingApi(UserScoreRankingRequestVO request, MaimaiDXUserScoreRankingHandler handler)
		{
			var response = await handler.GetUserScoreRanking(request);
			return Json(response);
		}

		[HttpPost("UploadUserPhotoApi")]
		public async ValueTask<IActionResult> UploadUserPhotoApi(UserPhotoRequestVO request, MaimaiDXUploadUserPhotoHandler handler)
		{
			var response = await handler.UploadUserPhoto(request);
			return Json(response);
		}

		[HttpPost("UploadUserPlaylogApi")]
		public async ValueTask<IActionResult> UploadUserPlaylogApi(UserPlaylogRequestVO request, MaimaiDXUploadUserPlaylogHandler handler)
		{
			var response = await handler.UploadUserPlaylog(request);
			return Json(response);
		}

		[HttpPost("UploadUserPortraitApi")]
		public async ValueTask<IActionResult> UploadUserPortraitApi(UploadUserPortraitRequestVO request, MaimaiDXUserPortraitHandler handler)
		{
			var response = await handler.UploadUserPortrait(request);
			return Json(response);
		}

		[HttpPost("UserLoginApi")]
		public async ValueTask<IActionResult> UserLoginApi(UserLoginRequestVO request, MaimaiDXUserLoginHandler handler)
		{
			var response = await handler.UserLogin(request);
			return Json(response);
		}

		[HttpPost("UserLogoutApi")]
		public async ValueTask<IActionResult> UserLogoutApi(UserLogoutRequestVO request, MaimaiDXUserLoginHandler handler)
		{
			var response = await handler.UserLogout(request);
			return Json(response);
		}

		[HttpPost("GetUserRivalMusicApi")]
		public async ValueTask<IActionResult> GetUserRivalMusicApi(UserRivalMusicRequestVO request, MaimaiDXUserRivalHandler handler)
		{
			var response = await handler.GetUserRivalMusic(request);
			return Json(response);
		}

		[HttpPost("GetUserRivalDataApi")]
		public async ValueTask<IActionResult> GetUserRivalDataApi(UserRivalRequestVO request, MaimaiDXUserRivalHandler handler)
		{
			var response = await handler.GetUserRivalData(request);
			return Json(response);
		}

		[HttpPost("UpsertClientBookkeepingApi")]
		public async ValueTask<IActionResult> UpsertClientBookkeepingApi(ClientBookkeepingRequestVO request, MaimaiDXClientBookkeepHandler handler)
		{
			var response = await handler.UpsertClientBookkeeping(request);
			return Json(response);
		}

		[HttpPost("UpsertClientSettingApi")]
		public async ValueTask<IActionResult> UpsertClientSettingApi(ClientSettingRequestVO request, MaimaiDXClientSettingHandler handler)
		{
			var response = await handler.UpsertClientSetting(request);
			return Json(response);
		}

		[HttpPost("UpsertClientTestmodeApi")]
		public async ValueTask<IActionResult> UpsertClientTestmodeApi(ClientTestmodeRequestVO request, MaimaiDXClientTestmodeHandler handler)
		{
			var response = await handler.UpsertClientTestmode(request);
			return Json(response);
		}

		[HttpPost("UpsertClientUploadApi")]
		public async ValueTask<IActionResult> UpsertClientUploadApi(ClientUploadRequestVO request, MaimaiDXClientUploadHandler handler)
		{
			var response = await handler.UpsertClientUpload(request);
			return Json(response);
		}

		[HttpPost("UpsertUserAllApi")]
		public async ValueTask<IActionResult> UpsertUserAllApi(UserAllRequestVO request, MaimaiDXUserAllHandler handler)
		{
			var response = await handler.UpsertUserAll(request);
			return Json(response);
		}

		[HttpPost("GetUserCourseApi")]
		public async ValueTask<IActionResult> GetUserCourseApi(UserCourseRequestVO request, MaimaiDXUserCourseHandler handler)
		{
			var response = await handler.GetUserCourse(request);
			return Json(response);
		}

		[HttpPost("GetUserFriendSeasonRankingApi")]
		public async ValueTask<IActionResult> GetUserFriendSeasonRankingApi(UserFriendSeasonRankingRequestVO request, MaimaiDXUserFriendSeasonRankingHandler handler)
		{
			var response = await handler.GetUserFriendSeasonRanking(request);
			return Json(response);
		}

		[HttpPost("GetGameChargeApi")]
		public async ValueTask<IActionResult> GetGameChargeApi(GameChargeRequestVO request, MaimaiDXGameChargeHandler handler)
		{
			var response = await handler.GetGameCharge(request);
			return Json(response);
		}

		[HttpPost("GetUserChargeApi")]
		public async ValueTask<IActionResult> GetUserChargeApi(UserChargeRequestVO request, MaimaiDXUserChargeHandler handler)
		{
			var response = await handler.GetUserCharge(request);
			return Json(response);
		}

		[HttpPost("UpsertUserChargelogApi")]
		public async ValueTask<IActionResult> UpsertUserChargelogApi(UserChargelogRequestVO request, MaimaiDXUserChargeHandler handler)
		{
			var response = await handler.UpsertUserChargelog(request);
			return Json(response);
		}

		[HttpPost("GetGameNgMusicIdApi")]
		public async ValueTask<IActionResult> GetGameNgMusicIdApi(GameNgMusicIdRequestVO request, MaimaiDXGameNgMusicIdHandler handler)
		{
			var response = await handler.GetGameNgMusicId(request);
			return Json(response);
		}
	}
}
