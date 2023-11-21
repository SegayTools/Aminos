namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserCardRequestVO
	{
		public ulong userId { get; set; }

		public int nextIndex { get; set; }

		public int maxCount { get; set; } = 20;
}
}
