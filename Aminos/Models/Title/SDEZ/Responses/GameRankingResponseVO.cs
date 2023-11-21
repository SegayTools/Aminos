using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class GameRankingResponseVO
	{
		public long type { get; set; }

		public GameRanking[] gameRankingList { get; set; }
	}
}
