using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserRegions")]
	public class UserRegion
	{
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int regionId { get; set; }

		public int playCount { get; set; }

		public string created { get; set; }
	}
}