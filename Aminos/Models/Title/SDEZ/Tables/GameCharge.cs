using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_GameCharges")]
    public class GameCharge
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int orderId;

        public int chargeId;

        public int price;

        public string startDate;

        public string endDate;
    }
}