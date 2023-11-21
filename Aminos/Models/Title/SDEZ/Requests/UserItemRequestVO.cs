namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserItemRequestVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public int maxCount { get; set; } = 100;
	}
}
