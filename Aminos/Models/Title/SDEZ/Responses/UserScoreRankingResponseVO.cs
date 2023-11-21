using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserScoreRankingResponseVO
	{
		public ulong userId { get; set; }

		public UserScoreRanking userScoreRanking { get; set; }
	}
}
