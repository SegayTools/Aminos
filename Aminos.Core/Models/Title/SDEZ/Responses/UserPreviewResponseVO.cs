using Aminos.Core.Utils.Json;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserPreviewResponseVO
	{
		public ulong userId { get; set; }

		public string userName { get; set; }

		public bool isLogin { get; set; }

		public string lastGameId { get; set; }

		public string lastDataVersion { get; set; }

		public string lastRomVersion { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime lastLoginDate { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime lastPlayDate { get; set; }

		public int playerRating { get; set; }

		public int nameplateId { get; set; }

		public int iconId { get; set; }

		public int trophyId { get; set; }

		public int partnerId { get; set; }

		public int frameId { get; set; }

		public int dispRate { get; set; }

		public int totalAwake { get; set; }

		public int isNetMember { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime dailyBonusDate { get; set; }

		public int headPhoneVolume { get; set; }

		public bool isInherit { get; set; }

		public int banState { get; set; }
	}
}
