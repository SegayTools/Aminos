namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserAllRequestVO
	{
		public ulong userId { get; set; }

		public ulong playlogId { get; set; }

		public bool isEventMode { get; set; }

		public bool isFreePlay { get; set; }

		public UserAll upsertUserAll { get; set; }
	}
}
