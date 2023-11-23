using Aminos.Utils.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserCards")]
	public class UserCard
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int cardId { get; set; }

		public int cardTypeId { get; set; }

		public int charaId { get; set; }

		public int mapId { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime startDate { get; set; }
		
		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime endDate { get; set; }
	}
}