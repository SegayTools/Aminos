namespace Aminos.Models.Title.SDEZ.Requests
{
	public class ClientSetting
	{
		public int placeId { get; set; }

		public string clientId { get; set; }

		public string placeName { get; set; }

		public int regionId { get; set; }

		public string regionName { get; set; }

		public string bordId { get; set; }

		public int romVersion { get; set; }

		public bool isDevelop { get; set; }

		public bool isAou { get; set; }
	}
}