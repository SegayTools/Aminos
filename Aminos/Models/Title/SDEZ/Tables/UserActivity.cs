using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserActivities")]
	public class UserActivity
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public ICollection<UserAct> playList { get; set; } = new List<UserAct>();
		public ICollection<UserAct> musicList { get; set; } = new List<UserAct>();
	}
}