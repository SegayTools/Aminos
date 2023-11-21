using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserActs")]
    public class UserAct
	{
		[JsonIgnore]
		public ulong UserActivityPlayListId { get; set; }
		[JsonIgnore]
		public ulong UserActivityMusicListId { get; set; }

		[Key]
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        public int kind { get; set; }

		public long sortNumber { get; set; }

		public int param1 { get; set; }

		public int param2 { get; set; }

		public int param3 { get; set; }

		public int param4 { get; set; }

		public int userId { get; set; }
	}
}