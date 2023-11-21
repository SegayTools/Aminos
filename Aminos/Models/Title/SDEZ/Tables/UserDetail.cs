using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserDetails")]
	[RegisterInjectable(typeof(IModelCreateBuilder<MaimaiDXDB>))]
	public class UserDetail : IModelCreateBuilder<MaimaiDXDB>
	{
		public void OnModelCreateBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.
				OneToMany<UserDetail, UserCard>(e => e.UserCards).
				OneToMany<UserDetail, UserCharacter>(e => e.UserCharacters).
				OneToMany<UserDetail, UserFavorite>(e => e.UserFavorites).
				OneToMany<UserDetail, UserFavoriteItem>(e => e.UserFavoriteItems).
				OneToMany<UserDetail, UserItem>(e => e.UserItems).
				OneToMany<UserDetail, UserLoginBonus>(e => e.UserLoginBonuses).
				OneToMany<UserDetail, UserMap>(e => e.UserMaps).
				OneToMany<UserDetail, UserMusicDetail>(e => e.UserMusicDetails).
				OneToMany<UserDetail, UserRegion>(e => e.UserRegions).
				OneToMany<UserDetail, UserScoreRanking>(e => e.UserScoreRankings).
				OneToMany<UserDetail, UserPlaylog>(e => e.UserPlaylogs).
				OneToMany<UserDetail, UserCourse>(e => e.UserCourses).
				OneToMany<UserDetail, UserFriendSeasonRanking>(e => e.UserFriendSeasonRankings).
				OneToMany<UserDetail, UserCharge>(e => e.UserCharges).
				OneToMany<UserDetail, UserChargelog>(e => e.UserChargelogs).
				OneToMany<UserDetail, UserGamePlaylog>(e => e.UserGamePlaylogs);

			modelBuilder.
				OneToOne<UserDetail, UserOption>(x => x.UserOption, x => x.UserDetailId).
				OneToOne<UserDetail, UserRating>(x => x.UserRating, x => x.UserDetailId).
				OneToOne<UserDetail, UserActivity>(x => x.UserActivity, x => x.UserDetailId).
				OneToOne<UserDetail, UserExtend>(x => x.UserExtend, x => x.UserDetailId);
		}

		[JsonIgnore]
		public ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();
		[JsonIgnore]
		public ICollection<UserCharacter> UserCharacters { get; set; } = new List<UserCharacter>();
		[JsonIgnore]
		public ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();
		[JsonIgnore]
		public ICollection<UserFavoriteItem> UserFavoriteItems { get; set; } = new List<UserFavoriteItem>();
		[JsonIgnore]
		public ICollection<UserItem> UserItems { get; set; } = new List<UserItem>();
		[JsonIgnore]
		public ICollection<UserLoginBonus> UserLoginBonuses { get; set; } = new List<UserLoginBonus>();
		[JsonIgnore]
		public ICollection<UserMap> UserMaps { get; set; } = new List<UserMap>();
		[JsonIgnore]
		public ICollection<UserMusicDetail> UserMusicDetails { get; set; } = new List<UserMusicDetail>();
		[JsonIgnore]
		public ICollection<UserRegion> UserRegions { get; set; } = new List<UserRegion>();
		[JsonIgnore]
		public ICollection<UserScoreRanking> UserScoreRankings { get; set; } = new List<UserScoreRanking>();
		[JsonIgnore]
		public ICollection<UserPlaylog> UserPlaylogs { get; set; } = new List<UserPlaylog>();
		[JsonIgnore]
		public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
		[JsonIgnore]
		public ICollection<UserFriendSeasonRanking> UserFriendSeasonRankings { get; set; } = new List<UserFriendSeasonRanking>();
		[JsonIgnore]
		public ICollection<UserCharge> UserCharges { get; set; } = new List<UserCharge>();
		[JsonIgnore]
		public ICollection<UserChargelog> UserChargelogs { get; set; } = new List<UserChargelog>();
		[JsonIgnore]
		public ICollection<UserGamePlaylog> UserGamePlaylogs { get; set; } = new List<UserGamePlaylog>();

		[JsonIgnore]
		public UserOption UserOption { get; set; }
		[JsonIgnore]
		public UserRating UserRating { get; set; }
		[JsonIgnore]
		public UserExtend UserExtend { get; set; }
		[JsonIgnore]
		public UserActivity UserActivity { get; set; }

		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public string accessCode { get; set; }

		public string userName { get; set; }

		public int isNetMember { get; set; }

		public int iconId { get; set; }

		public int plateId { get; set; }

		public int titleId { get; set; }

		public int partnerId { get; set; }

		public int frameId { get; set; }

		public int selectMapId { get; set; }

		public int totalAwake { get; set; }

		public int gradeRating { get; set; }

		public int musicRating { get; set; }

		public int playerRating { get; set; }

		public int highestRating { get; set; }

		public int gradeRank { get; set; }

		public int classRank { get; set; }

		public int courseRank { get; set; }

		[Column(nameof(charaSlot))]
		[MaxLength(256)]
		private string __charaSlot { get; set; }
		[NotMapped]
		public int[] charaSlot
		{
			get => __charaSlot.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			set => __charaSlot = string.Join(",", value);
		}

		[Column(nameof(charaLockSlot))]
		[MaxLength(256)]
		private string __charaLockSlot { get; set; }
		[NotMapped]
		public int[] charaLockSlot
		{
			get => __charaLockSlot.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			set => __charaLockSlot = string.Join(",", value);
		}

		public ulong contentBit { get; set; }

		public int playCount { get; set; }

		public int currentPlayCount { get; set; }

		public int renameCredit { get; set; }

		public int mapStock { get; set; }

		public string eventWatchedDate { get; set; }

		public string lastGameId { get; set; }

		public string lastRomVersion { get; set; }

		public string lastDataVersion { get; set; }

		public string lastLoginDate { get; set; }

		public string lastPlayDate { get; set; }

		public int lastPlayCredit { get; set; }

		public int lastPlayMode { get; set; }

		public int lastPlaceId { get; set; }

		public string lastPlaceName { get; set; }

		public int lastAllNetId { get; set; }

		public int lastRegionId { get; set; }

		public string lastRegionName { get; set; }

		public string lastClientId { get; set; }

		public string lastCountryCode { get; set; }

		public int lastSelectEMoney { get; set; }

		public int lastSelectTicket { get; set; }

		public int lastSelectCourse { get; set; }

		public int lastCountCourse { get; set; }

		public string firstGameId { get; set; }

		public string firstRomVersion { get; set; }

		public string firstDataVersion { get; set; }

		public string firstPlayDate { get; set; }

		public string compatibleCmVersion { get; set; }

		public string dailyBonusDate { get; set; }

		public string dailyCourseBonusDate { get; set; }

		public string lastPairLoginDate { get; set; }

		public string lastTrialPlayDate { get; set; }

		public int playVsCount { get; set; }

		public int playSyncCount { get; set; }

		public int winCount { get; set; }

		public int helpCount { get; set; }

		public int comboCount { get; set; }

		public long totalDeluxscore { get; set; }

		public long totalBasicDeluxscore { get; set; }

		public long totalAdvancedDeluxscore { get; set; }

		public long totalExpertDeluxscore { get; set; }

		public long totalMasterDeluxscore { get; set; }

		public long totalReMasterDeluxscore { get; set; }

		public int totalSync { get; set; }

		public int totalBasicSync { get; set; }

		public int totalAdvancedSync { get; set; }

		public int totalExpertSync { get; set; }

		public int totalMasterSync { get; set; }

		public int totalReMasterSync { get; set; }

		public long totalAchievement { get; set; }

		public long totalBasicAchievement { get; set; }

		public long totalAdvancedAchievement { get; set; }

		public long totalExpertAchievement { get; set; }

		public long totalMasterAchievement { get; set; }

		public long totalReMasterAchievement { get; set; }

		public long playerOldRating { get; set; }

		public long playerNewRating { get; set; }

		public int banState { get; set; }

		public long dateTime { get; set; }
	}
}
