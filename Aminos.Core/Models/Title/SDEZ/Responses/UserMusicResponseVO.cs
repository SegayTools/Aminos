namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserMusicResponseVO
	{
		public ulong userId { get; set; }

		public int nextIndex { get; set; }

		public UserMusic[] userMusicList { get; set; }
	}
}
