using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Requests
{
    public class UserAll
	{
		public UserDetail[] userData;

		public UserExtend[] userExtend;

		public UserOption[] userOption;

		public UserCharacter[] userCharacterList;

		public UserGhost[] userGhost;

		public UserMap[] userMapList;

		public UserLoginBonus[] userLoginBonusList;

		public UserRating[] userRatingList;

		public UserItem[] userItemList;

		public UserMusicDetail[] userMusicDetailList;

		public UserCourse[] userCourseList;

		public UserFriendSeasonRanking[] userFriendSeasonRankingList;

		public UserCharge[] userChargeList;

		public UserFavorite[] userFavoriteList;

		public UserActivity[] userActivityList;

		public UserGamePlaylog[] userGamePlaylogList;

		public User2pPlaylog user2pPlaylog;

		public string isNewCharacterList;

		public string isNewMapList;

		public string isNewLoginBonusList;

		public string isNewItemList;

		public string isNewMusicDetailList;

		public string isNewCourseList;

		public string isNewFavoriteList;

		public string isNewFriendSeasonRankingList;
	}
}