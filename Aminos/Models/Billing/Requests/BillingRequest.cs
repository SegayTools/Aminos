namespace Aminos.Models.Billing.Requests
{
	public class BillingRequest : QueryPathSerializeBase
	{
		public string keychipid { get; set; }
		public string functype { get; set; }
		public string gameid { get; set; }
		public string gamever { get; set; }
		public string boardid { get; set; }
		public string tenpoip { get; set; }
		public string libalibver { get; set; }
		public string datamax { get; set; }
		public string billingtype { get; set; }
		public string protocolver { get; set; }
		public string operatingfix { get; set; }
		public string traceleft { get; set; }
		public string requestno { get; set; }
		public string datesync { get; set; }
		public string timezone { get; set; }
		public string date { get; set; }
		public string crcerrcnt { get; set; }
		public string memrepair { get; set; }
		public string playcnt { get; set; }
		public string playlimit { get; set; }
		public string nearfull { get; set; }
	}
}
