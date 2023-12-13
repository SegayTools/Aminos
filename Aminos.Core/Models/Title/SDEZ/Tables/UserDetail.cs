using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Utils.Json;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_UserDetails")]
public class UserDetail
{
    [JsonIgnore]
    public virtual ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();

    [JsonIgnore]
    public virtual ICollection<UserCharacter> UserCharacters { get; set; } = new List<UserCharacter>();

    [JsonIgnore]
    public virtual ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();

    [JsonIgnore]
    public virtual ICollection<UserFavoriteItem> UserFavoriteItems { get; set; } = new List<UserFavoriteItem>();

    [JsonIgnore]
    public virtual ICollection<UserItem> UserItems { get; set; } = new List<UserItem>();

    [JsonIgnore]
    public virtual ICollection<UserLoginBonus> UserLoginBonuses { get; set; } = new List<UserLoginBonus>();

    [JsonIgnore]
    public virtual ICollection<UserMap> UserMaps { get; set; } = new List<UserMap>();

    [JsonIgnore]
    public virtual ICollection<UserMusicDetail> UserMusicDetails { get; set; } = new List<UserMusicDetail>();

    [JsonIgnore]
    public virtual ICollection<UserRegion> UserRegions { get; set; } = new List<UserRegion>();

    [JsonIgnore]
    public virtual ICollection<UserScoreRanking> UserScoreRankings { get; set; } = new List<UserScoreRanking>();

    [JsonIgnore]
    public virtual ICollection<UserPlaylog> UserPlaylogs { get; set; } = new List<UserPlaylog>();

    [JsonIgnore]
    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    [JsonIgnore]
    public virtual ICollection<UserFriendSeasonRanking> UserFriendSeasonRankings { get; set; } =
        new List<UserFriendSeasonRanking>();

    [JsonIgnore]
    public virtual ICollection<UserCharge> UserCharges { get; set; } = new List<UserCharge>();

    [JsonIgnore]
    public virtual ICollection<UserChargelog> UserChargelogs { get; set; } = new List<UserChargelog>();

    [JsonIgnore]
    public virtual ICollection<UserGamePlaylog> UserGamePlaylogs { get; set; } = new List<UserGamePlaylog>();

    [JsonIgnore]
    public virtual UserOption UserOption { get; set; }

    [JsonIgnore]
    public virtual UserExtend UserExtend { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();

    [JsonIgnore]
    public bool IsGuest => (Id & 0x1000000000001L) == 281474976710657L;

    [JsonIgnore]
    public ulong AimeId => Id;

    [System.ComponentModel.DataAnnotations.Key]
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
    public byte[] __charaSlot { get; set; }

    [NotMapped]
    public int[] charaSlot
    {
        get => __charaSlot is null
            ? new int[0]
            : MessagePackSerializer.Deserialize<int[]>(__charaSlot);
        set => __charaSlot = MessagePackSerializer.Serialize(value);
    }

    [Column(nameof(charaLockSlot))]
    [MaxLength(256)]
    public byte[] __charaLockSlot { get; set; }

    [NotMapped]
    public int[] charaLockSlot
    {
        get => __charaLockSlot is null
            ? new int[0]
            : MessagePackSerializer.Deserialize<int[]>(__charaLockSlot);
        set => __charaLockSlot = MessagePackSerializer.Serialize(value);
    }
    
    [Column("userRating")]
    [MaxLength(8192)]
    [JsonIgnore]
    public byte[] __userRating { get; set; }

    [NotMapped]
    [JsonIgnore]
    public UserRating UserRating
    {
        get => __userRating is null
            ? default
            : MessagePackSerializer.Deserialize<UserRating>(__userRating);
        set => __userRating = MessagePackSerializer.Serialize(value);
    }

    public ulong contentBit { get; set; }

    public int playCount { get; set; }

    public int currentPlayCount { get; set; }

    public int renameCredit { get; set; }

    public int mapStock { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime eventWatchedDate { get; set; }

    public string lastGameId { get; set; }

    public string lastRomVersion { get; set; }

    public string lastDataVersion { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime lastLoginDate { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime lastPlayDate { get; set; }

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

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime firstPlayDate { get; set; }

    public string compatibleCmVersion { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime dailyBonusDate { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime dailyCourseBonusDate { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime lastPairLoginDate { get; set; }

    [JsonConverter(typeof(TitleString2DateTimeConverter))]
    public DateTime lastTrialPlayDate { get; set; }

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