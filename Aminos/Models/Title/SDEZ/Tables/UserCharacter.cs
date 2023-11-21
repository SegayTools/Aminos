using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCharacters")]
    public class UserCharacter
	{
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
        public ulong Id { get; set; }

        public int characterId { get; set; }

		public uint level { get; set; }

		public uint awakening { get; set; }

		public uint useCount { get; set; }
    }
}