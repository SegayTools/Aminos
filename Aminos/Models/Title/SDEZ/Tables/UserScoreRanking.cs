using Aminos.Utils.Json;
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
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int tournamentId { get; set; }

		public long totalScore { get; set; }

		public int ranking { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime rankingDate { get; set; }
	}
}