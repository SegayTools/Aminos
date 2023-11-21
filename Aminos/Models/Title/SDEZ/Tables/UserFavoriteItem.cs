using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(UserFavoriteItemId))]
    [Table("MaimaiDX_UserFavoriteItems")]
    public class UserFavoriteItem
    {
        [Key]
        [JsonPropertyName("orderId")]
        public int UserFavoriteItemId { get; set; }

        public int kind;

        public ulong id;
    }
}