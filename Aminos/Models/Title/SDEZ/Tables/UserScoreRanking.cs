using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserScoreRankings")]
	public class UserScoreRanking
	{
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int tournamentId { get; set; }

		public long totalScore { get; set; }

		public int ranking { get; set; }

		public string rankingDate { get; set; }
	}
}