namespace Aminos.Models.AllNet.Responses
{
	public class PowerOnResponseV3 : PowerOnResponseBase
	{
		public string allnet_id { get; set; }
		public string client_timezone { get; set; }
		public string utc_time { get; set; }
		public string setting { get; set; }
		public string res_ver { get; set; }
		public string token { get; set; }
	}
}
