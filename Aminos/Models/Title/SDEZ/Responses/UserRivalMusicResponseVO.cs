namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserRivalMusicResponseVO
	{
		public ulong userId { get; set; }

		public ulong rivalId { get; set; }

		public int nextIndex { get; set; }

		public UserRivalMusic[] userRivalMusicList { get; set; }
	}
}
