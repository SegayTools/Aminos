namespace Aminos.Models.Title.SDEZ.Responses
{
	public class GameTournamentInfo
	{
		public int tournamentId { get; set; }

		public string tournamentName { get; set; }

		public int rankingKind { get; set; }

		public int scoreType { get; set; }

		public string noticeStartDate { get; set; }

		public string noticeEndDate { get; set; }

		public string startDate { get; set; }

		public string endDate { get; set; }

		public string entryStartDate { get; set; }

		public string entryEndDate { get; set; }

		public GameTournamentMusic[] gameTournamentMusicList { get; set; }
	}
}