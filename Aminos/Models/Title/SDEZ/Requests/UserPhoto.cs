namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserPhoto
	{
		public int orderId { get; set; }

		public ulong userId { get; set; }

		public int divNumber { get; set; }

		public int divLength { get; set; }

		public string divData { get; set; }

		public int placeId { get; set; }

		public string clientId { get; set; }

		public string uploadDate { get; set; }

		public ulong playlogId { get; set; }

		public int trackNo { get; set; }
	}
}