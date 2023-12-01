namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserMusicRequestVO
	{
		public ulong userId { get; set; }

		public int nextIndex { get; set; }

		public int maxCount { get; set; } = 50;
	}
}
