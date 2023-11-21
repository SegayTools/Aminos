using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_GameEvents")]
	public class GameEvent
	{
		[Key]
		[JsonPropertyName("id")]
		public ulong Id { get; set; }

		public int type { get; set; }

		public string startDate { get; set; }

		public string endDate { get; set; }

		[JsonIgnore]
		public bool enable { get; set; }
	}
}
