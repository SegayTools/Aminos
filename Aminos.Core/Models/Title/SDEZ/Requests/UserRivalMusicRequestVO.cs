namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserRivalMusicRequestVO
	{
		public ulong userId { get; set; }

		public ulong rivalId { get; set; }

		public int nextIndex { get; set; }

		public UserRivalMusicLevel[] userRivalMusicLevelList { get; set; } = new UserRivalMusicLevel[5]
		{
			new UserRivalMusicLevel
			{
				level = 0
			},
			new UserRivalMusicLevel
			{
				level = 1
			},
			new UserRivalMusicLevel
			{
				level = 2
			},
			new UserRivalMusicLevel
			{
				level = 3
			},
			new UserRivalMusicLevel
			{
				level = 4
			}
		};
	}
}
