using Aminos.Utils.Json;
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

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime startDate { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime endDate { get; set; }
	}
}