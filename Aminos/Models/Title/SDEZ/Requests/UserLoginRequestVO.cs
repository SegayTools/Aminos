namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserLoginRequestVO
	{
		public ulong userId { get; set; }

		public string accessCode { get; set; }

		public int regionId { get; set; }

		public int placeId { get; set; }

		public string clientId { get; set; }

		public long dateTime { get; set; }
	}
}
