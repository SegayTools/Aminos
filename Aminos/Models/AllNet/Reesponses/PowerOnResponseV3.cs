using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Reesponses
{
	public class PowerOnResponseV3 : PowerOnResponseBase
	{
		[JsonPropertyName("allnet_id")]
		public string AllnetId { get; set; }
		[JsonPropertyName("client_timezone")]
		public string ClientTimezone { get; set; }
		[JsonPropertyName("utc_time")]
		public string UtcTime { get; set; }
		[JsonPropertyName("setting")]
		public string Setting { get; set; }
		[JsonPropertyName("res_ver")]
		public string ResVersion { get; set; }
		[JsonPropertyName("token")]
		public string Token { get; set; }
	}
}
