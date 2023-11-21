using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Responses
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserFriendSeasonRankings")]
	public class UserFriendSeasonRanking
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int seasonId { get; set; }

		public int point { get; set; }

		public int rank { get; set; }

		public bool rewardGet { get; set; }

		public string userName { get; set; }

		public string recordDate { get; set; }
	}
}