using Aminos.Core.Utils.Json;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserLoginResponseVO
	{
		public int returnCode { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime lastLoginDate { get; set; }

		public int loginCount { get; set; }

		public int consecutiveLoginCount { get; set; }

		public ulong loginId { get; set; }

		public string Bearer { get; set; }
	}
}
