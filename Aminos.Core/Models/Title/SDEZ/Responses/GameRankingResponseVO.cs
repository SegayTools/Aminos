using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class GameRankingResponseVO
	{
		public long type { get; set; }

		public GameRanking[] gameRankingList { get; set; }
	}
}
