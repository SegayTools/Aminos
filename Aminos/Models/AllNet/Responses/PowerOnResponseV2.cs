using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Responses
{
	public class PowerOnResponseV2 : PowerOnResponseBase
	{
		public int year { get; set; }
		public int month { get; set; }
		public int day { get; set; }
		public int hour { get; set; }
		public int minute { get; set; }
		public int second { get; set; }
		public string setting { get; set; } = "1";
		public string timezone { get; set; } = "+09:00";
		public string res_class { get; set; } = "PowerOnResponseV2";
	}
}
