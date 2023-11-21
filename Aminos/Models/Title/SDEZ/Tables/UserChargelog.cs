using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{

    [Index(nameof(Id))]
    [Table("MaimaiDX_UserChargelogs")]
    public class UserChargelog
	{
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
        [JsonIgnore]
        public ulong Id { get; set; }

        public int chargeId { get; set; }

		public int price { get; set; }

		public string purchaseDate { get; set; }

		public int playCount { get; set; }

		public int playerRating { get; set; }

		public int placeId { get; set; }

		public int regionId { get; set; }

		public string clientId { get; set; }
	}
}