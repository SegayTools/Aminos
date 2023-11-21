using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserRates")]
	public class UserRate
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int musicId { get; set; }

		public int level { get; set; }

		public uint romVersion { get; set; }

		public uint achievement { get; set; }
	}
}