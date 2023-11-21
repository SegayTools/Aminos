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
        public ICollection<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }

        public ulong userId1;

        public ulong userId2;

        public string userName1;

        public string userName2;

        public int regionId;

        public int placeId;
    }
}