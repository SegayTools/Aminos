namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserRivalMusicRequestVO
	{
		public ulong userId;

		public ulong rivalId;

		public int nextIndex;

		public UserRivalMusicLevel[] userRivalMusicLevelList = new UserRivalMusicLevel[5]
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
