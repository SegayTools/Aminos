namespace Aminos.Core.Models.Billing.Responses
{
	public class BillingResponse : QueryPathSerializeBase
	{
		public int result { get; set; }
		public int waittime { get; set; }
		public int linelimit { get; set; }
		public string message { get; set; }
		public int playlimit { get; set; }
		public string playlimitsig { get; set; }
		public string protocolver { get; set; }
		public int nearfull { get; set; }
		public string nearfullsig { get; set; }
		public int fixlogcnt { get; set; }
		public int fixinterval { get; set; }
		public string playhistory { get; set; }
	}
}
