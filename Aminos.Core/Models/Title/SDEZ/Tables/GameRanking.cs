using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(id))]
	[Table("MaimaiDX_GameRankings")]
	public class GameRanking
	{
		[Key]
		public long id { get; set; }

		public long point { get; set; }

		public string userName { get; set; }
	}
}