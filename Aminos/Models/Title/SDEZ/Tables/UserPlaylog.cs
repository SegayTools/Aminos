using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserPlaylogs")]
	public class UserPlaylog
	{
		[JsonIgnore]
		[Key]
		public ulong Id { get; set; }

		public ulong userId { get; set; }

		public int orderId { get; set; }

		public ulong playlogId { get; set; }

		public int version { get; set; }

		public int placeId { get; set; }

		public string placeName { get; set; }

		public long loginDate { get; set; }

		public string playDate { get; set; }

		public string userPlayDate { get; set; }

		public int type { get; set; }

		public int musicId { get; set; }

		public int level { get; set; }

		public int trackNo { get; set; }

		public int vsMode { get; set; }

		public string vsUserName { get; set; }

		public int vsStatus { get; set; }

		public int vsUserRating { get; set; }

		public int vsUserAchievement { get; set; }

		public int vsUserGradeRank { get; set; }

		public int vsRank { get; set; }

		public int playerNum { get; set; }

		public ulong playedUserId1 { get; set; }

		public string playedUserName1 { get; set; }

		public int playedMusicLevel1 { get; set; }

		public ulong playedUserId2 { get; set; }

		public string playedUserName2 { get; set; }

		public int playedMusicLevel2 { get; set; }

		public ulong playedUserId3 { get; set; }

		public string playedUserName3 { get; set; }

		public int playedMusicLevel3 { get; set; }

		public int characterId1 { get; set; }

		public int characterLevel1 { get; set; }

		public int characterAwakening1 { get; set; }

		public int characterId2 { get; set; }

		public int characterLevel2 { get; set; }

		public int characterAwakening2 { get; set; }

		public int characterId3 { get; set; }

		public int characterLevel3 { get; set; }

		public int characterAwakening3 { get; set; }

		public int characterId4 { get; set; }

		public int characterLevel4 { get; set; }

		public int characterAwakening4 { get; set; }

		public int characterId5 { get; set; }

		public int characterLevel5 { get; set; }

		public int characterAwakening5 { get; set; }

		public int achievement { get; set; }

		public int deluxscore { get; set; }

		public int scoreRank { get; set; }

		public int maxCombo { get; set; }

		public int totalCombo { get; set; }

		public int maxSync { get; set; }

		public int totalSync { get; set; }

		public int tapCriticalPerfect { get; set; }

		public int tapPerfect { get; set; }

		public int tapGreat { get; set; }

		public int tapGood { get; set; }

		public int tapMiss { get; set; }

		public int holdCriticalPerfect { get; set; }

		public int holdPerfect { get; set; }

		public int holdGreat { get; set; }

		public int holdGood { get; set; }

		public int holdMiss { get; set; }

		public int slideCriticalPerfect { get; set; }

		public int slidePerfect { get; set; }

		public int slideGreat { get; set; }

		public int slideGood { get; set; }

		public int slideMiss { get; set; }

		public int touchCriticalPerfect { get; set; }

		public int touchPerfect { get; set; }

		public int touchGreat { get; set; }

		public int touchGood { get; set; }

		public int touchMiss { get; set; }

		public int breakCriticalPerfect { get; set; }

		public int breakPerfect { get; set; }

		public int breakGreat { get; set; }

		public int breakGood { get; set; }

		public int breakMiss { get; set; }

		public bool isTap { get; set; }

		public bool isHold { get; set; }

		public bool isSlide { get; set; }

		public bool isTouch { get; set; }

		public bool isBreak { get; set; }

		public bool isCriticalDisp { get; set; }

		public bool isFastLateDisp { get; set; }

		public int fastCount { get; set; }

		public int lateCount { get; set; }

		public bool isAchieveNewRecord { get; set; }

		public bool isDeluxscoreNewRecord { get; set; }

		public int comboStatus { get; set; }

		public int syncStatus { get; set; }

		public bool isClear { get; set; }

		public int beforeRating { get; set; }

		public int afterRating { get; set; }

		public int beforeGrade { get; set; }

		public int afterGrade { get; set; }

		public int afterGradeRank { get; set; }

		public int beforeDeluxRating { get; set; }

		public int afterDeluxRating { get; set; }

		public bool isPlayTutorial { get; set; }

		public bool isEventMode { get; set; }

		public bool isFreedomMode { get; set; }

		public int playMode { get; set; }

		public bool isNewFree { get; set; }

		public int trialPlayAchievement { get; set; }

		public int extNum1 { get; set; }

		public int extNum2 { get; set; }

		public int extNum4 { get; set; }

		public bool extBool1 { get; set; }
	}
}