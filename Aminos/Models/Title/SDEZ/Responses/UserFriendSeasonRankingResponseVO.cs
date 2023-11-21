namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserFriendSeasonRankingResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public UserFriendSeasonRanking[] userFriendSeasonRankingList { get; set; }
	}
}
