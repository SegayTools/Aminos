using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Reesponses
{
	public class PowerOnResponseV2 : PowerOnResponseBase
	{
		[JsonPropertyName("year")]
		public int Year{ get; set; }
		[JsonPropertyName("month")]
		public int Month{ get; set; }
		[JsonPropertyName("day")]
		public int Day{ get; set; }
		[JsonPropertyName("hour")]
		public int Hour{ get; set; }
		[JsonPropertyName("minute")]
		public int Minute{ get; set; }
		[JsonPropertyName("second")]
		public int Second{ get; set; }
		[JsonPropertyName("setting")]
		public string Setting { get; set; } = "1";
		[JsonPropertyName("timezone")]
		public string Timezone { get; set; } = "+09:00";
		[JsonPropertyName("res_class")]
		public string ResClass { get; set; } = "PowerOnResponseV2";
	}
}
