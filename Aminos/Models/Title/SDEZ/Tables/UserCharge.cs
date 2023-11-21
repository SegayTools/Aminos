using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCharges")]
    public class UserCharge
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int chargeId;

        public int stock;

        public string purchaseDate;

        public string validDate;
    }
}