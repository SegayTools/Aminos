using Aminos.Core.Utils.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserCharges")]
	public class UserCharge
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int chargeId { get; set; }

		public int stock { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime purchaseDate { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime validDate { get; set; }
	}
}