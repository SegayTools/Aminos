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
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
        [JsonPropertyName("orderId")]
        public int UserFavoriteItemId { get; set; }

        public int kind { get; set; }

		public ulong id { get; set; }
	}
}