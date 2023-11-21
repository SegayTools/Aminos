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

		public int seasonId;

		public int point;

		public int rank;

		public bool rewardGet;

		public string userName;

		public string recordDate;
	}
}