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
        public ulong Id { get; set; }

        public int orderId { get; set; }

        public int chargeId { get; set; }

		public int price { get; set; }

		public string startDate { get; set; }

		public string endDate { get; set; }
	}
}