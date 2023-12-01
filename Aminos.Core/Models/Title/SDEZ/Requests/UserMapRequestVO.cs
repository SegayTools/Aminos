namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserMapRequestVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public int maxCount { get; set; } = 20;
	}
}
