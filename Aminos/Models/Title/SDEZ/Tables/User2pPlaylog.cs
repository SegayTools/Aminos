using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_User2pPlaylogs")]
    public class User2pPlaylog
    {
        [Key]
        public int Id { set; get; }

        [JsonInclude]
        [JsonPropertyName("user2pPlaylogDetailList")]
        public ICollection<User2pPlaylogDetail> User2pPlaylogDetails { get; set; } = new List<User2pPlaylogDetail>();

		public ulong userId1 { get; set; }

		public ulong userId2 { get; set; }

		public string userName1 { get; set; }

		public string userName2 { get; set; }

		public int regionId { get; set; }

		public int placeId { get; set; }
	}
}