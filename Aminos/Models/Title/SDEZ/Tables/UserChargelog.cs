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
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int chargeId;

        public int price;

        public string purchaseDate;

        public int playCount;

        public int playerRating;

        public int placeId;

        public int regionId;

        public string clientId;
    }
}