using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserDetails")]
    public class UserDetail
    {
        [JsonIgnore]
        public ICollection<UserCard> UserCards { get; set; }
        [JsonIgnore]
        public ICollection<UserCharacter> UserCharacters { get; set; }
        [JsonIgnore]
        public UserActivity UserActivity { get; set; }
        [JsonIgnore]
        public ICollection<UserFavorite> UserFavorites { get; set; }
        [JsonIgnore]
        public ICollection<UserFavoriteItem> UserFavoriteItems { get; set; }
        [JsonIgnore]
        public ICollection<UserItem> UserItems { get; set; }
        [JsonIgnore]
        public ICollection<UserLoginBonus> UserLoginBonuses { get; set; }
        [JsonIgnore]
        public ICollection<UserMap> UserMaps { get; set; }
        [JsonIgnore]
        public ICollection<UserMusicDetail> UserMusicDetails { get; set; }
        [JsonIgnore]
        public ICollection<UserRegion> UserRegions { get; set; }
        [JsonIgnore]
        public ICollection<UserScoreRanking> UserScoreRankings { get; set; }
        [JsonIgnore]
        public ICollection<UserPlaylog> UserPlaylogs { get; set; }
        [JsonIgnore]
        public ICollection<UserCourse> UserCourses { get; set; }
        [JsonIgnore]
        public ICollection<UserFriendSeasonRanking> UserFriendSeasonRankings { get; set; }
        [JsonIgnore]
        public ICollection<UserCharge> UserCharges { get; set; }
        [JsonIgnore]
        public ICollection<UserChargelog> UserChargelogs { get; set; }
        [JsonIgnore]
        public ICollection<UserGamePlaylog> UserGamePlaylogs { get; set; }

        [JsonIgnore]
        public UserOption UserOption { get; set; }
        [JsonIgnore]
        public UserRating UserRating { get; set; }
        [JsonIgnore]
        public UserExtend UserExtend { get; set; }


        [Key]
        [JsonIgnore]
        public ulong Id { get; set; }

        public string accessCode;

        public string userName;

        public int isNetMember;

        public int iconId;

        public int plateId;

        public int titleId;

        public int partnerId;

        public int frameId;

        public int selectMapId;

        public int totalAwake;

        public int gradeRating;

        public int musicRating;

        public int playerRating;

        public int highestRating;

        public int gradeRank;

        public int classRank;

        public int courseRank;

        public int[] charaSlot;

        public int[] charaLockSlot;

        public ulong contentBit;

        public int playCount;

        public int currentPlayCount;

        public int renameCredit;

        public int mapStock;

        public string eventWatchedDate;

        public string lastGameId;

        public string lastRomVersion;

        public string lastDataVersion;

        public string lastLoginDate;

        public string lastPlayDate;

        public int lastPlayCredit;

        public int lastPlayMode;

        public int lastPlaceId;

        public string lastPlaceName;

        public int lastAllNetId;

        public int lastRegionId;

        public string lastRegionName;

        public string lastClientId;

        public string lastCountryCode;

        public int lastSelectEMoney;

        public int lastSelectTicket;

        public int lastSelectCourse;

        public int lastCountCourse;

        public string firstGameId;

        public string firstRomVersion;

        public string firstDataVersion;

        public string firstPlayDate;

        public string compatibleCmVersion;

        public string dailyBonusDate;

        public string dailyCourseBonusDate;

        public string lastPairLoginDate;

        public string lastTrialPlayDate;

        public int playVsCount;

        public int playSyncCount;

        public int winCount;

        public int helpCount;

        public int comboCount;

        public long totalDeluxscore;

        public long totalBasicDeluxscore;

        public long totalAdvancedDeluxscore;

        public long totalExpertDeluxscore;

        public long totalMasterDeluxscore;

        public long totalReMasterDeluxscore;

        public int totalSync;

        public int totalBasicSync;

        public int totalAdvancedSync;

        public int totalExpertSync;

        public int totalMasterSync;

        public int totalReMasterSync;

        public long totalAchievement;

        public long totalBasicAchievement;

        public long totalAdvancedAchievement;

        public long totalExpertAchievement;

        public long totalMasterAchievement;

        public long totalReMasterAchievement;

        public long playerOldRating;

        public long playerNewRating;

        public int banState;

        public long dateTime;
    }
}
