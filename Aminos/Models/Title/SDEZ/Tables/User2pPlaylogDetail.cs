using Aminos.Utils.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_User2pPlaylogDetails")]
	public class User2pPlaylogDetail
	{
		[JsonIgnore]
		[Key]
		public int Id { set; get; }

		[JsonIgnore]
		public virtual UserDetail UserDetail1 { get; set; }

		[JsonIgnore]
		public virtual UserDetail UserDetail2 { get; set; }

		public int musicId { get; set; }

		public int level { get; set; }

		public int achievement { get; set; }

		public int deluxscore { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime userPlayDate { get; set; }
	}
}