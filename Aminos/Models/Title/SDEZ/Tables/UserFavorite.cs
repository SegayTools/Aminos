using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserFavorites")]
    public class UserFavorite
    {
        [JsonIgnore]
        public UserDetail UserDetail { get; set; }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public ulong userId => UserDetail?.Id ?? 0;

        public int itemKind;

        [NotMapped]
        public int[] itemId
        {
            get => __itemId.Split(';').Select(int.Parse).ToArray();
            set => __itemId = string.Join(";", value);
        }

        [JsonIgnore]
        [Column("itemId")]
        public string __itemId { get; set; }
    }
}