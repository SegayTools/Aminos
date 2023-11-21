using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserLoginBonuses")]
	public class UserLoginBonus
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int bonusId { get; set; }

		public uint point { get; set; }

		public bool isCurrent { get; set; }

		public bool isComplete { get; set; }
	}
}