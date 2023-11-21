using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserAll
	{
		public UserDetail[] userData { get; set; }

		public UserExtend[] userExtend { get; set; }

		public UserOption[] userOption { get; set; }

		public UserCharacter[] userCharacterList { get; set; }

		public UserGhost[] userGhost { get; set; }

		public UserMap[] userMapList { get; set; }

		public UserLoginBonus[] userLoginBonusList { get; set; }

		public UserRating[] userRatingList { get; set; }

		public UserItem[] userItemList { get; set; }

		public UserMusicDetail[] userMusicDetailList { get; set; }

		public UserCourse[] userCourseList { get; set; }

		public UserFriendSeasonRanking[] userFriendSeasonRankingList { get; set; }

		public UserCharge[] userChargeList { get; set; }

		public UserFavorite[] userFavoriteList { get; set; }

		public UserActivity[] userActivityList { get; set; }

		public UserGamePlaylog[] userGamePlaylogList { get; set; }

		public User2pPlaylog user2pPlaylog { get; set; }

		public string isNewCharacterList { get; set; }

		public string isNewMapList { get; set; }

		public string isNewLoginBonusList { get; set; }

		public string isNewItemList { get; set; }

		public string isNewMusicDetailList { get; set; }

		public string isNewCourseList { get; set; }

		public string isNewFavoriteList { get; set; }

		public string isNewFriendSeasonRankingList { get; set; }
	}
}