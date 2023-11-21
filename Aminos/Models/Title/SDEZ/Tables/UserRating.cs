using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserRatings")]
	public class UserRating
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int rating { get; set; }

		public ICollection<UserRate> ratingList { get; set; } = new List<UserRate>();
		public ICollection<UserRate> newRatingList { get; set; } = new List<UserRate>();
		public ICollection<UserRate> nextRatingList { get; set; } = new List<UserRate>();
		public ICollection<UserRate> nextNewRatingList { get; set; } = new List<UserRate>();

		public UserUdemae udemae { get; set; }
	}
}