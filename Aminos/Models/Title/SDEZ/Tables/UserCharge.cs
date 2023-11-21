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
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
        [JsonIgnore]
        public ulong Id { get; set; }

        public int chargeId { get; set; }

		public int stock { get; set; }

		public string purchaseDate { get; set; }

		public string validDate { get; set; }
	}
}