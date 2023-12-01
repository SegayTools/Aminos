namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class GameSetting
	{
		public bool isMaintenance { get; set; }

		public int requestInterval { get; set; }

		public string rebootStartTime { get; set; }

		public string rebootEndTime { get; set; }

		public int movieUploadLimit { get; set; }

		public int movieStatus { get; set; }

		public string movieServerUri { get; set; }

		public string deliverServerUri { get; set; }

		public string oldServerUri { get; set; }

		public string usbDlServerUri { get; set; }

		public int rebootInterval { get; set; }
	}
}