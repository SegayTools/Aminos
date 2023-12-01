using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserScoreRankingResponseVO
	{
		public ulong userId { get; set; }

		public UserScoreRanking userScoreRanking { get; set; }
	}
}
