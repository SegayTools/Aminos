namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class TransferFriend
	{
		public ulong playUserId { get; set; }

		public string playUserName { get; set; }

		public string playDate { get; set; }

		public int friendPoint { get; set; }

		public bool isFavorite { get; set; }
	}
}