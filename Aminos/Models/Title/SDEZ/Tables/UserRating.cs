using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserRatings")]
	[RegisterInjectable(typeof(IModelCreateBuilder<MaimaiDXDB>))]
	public class UserRating : IModelCreateBuilder<MaimaiDXDB>
	{
		public void OnModelCreateBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder
				.OneToOne<UserRating, UserUdemae>(x => x.udemae, x => x.UserRatingId)

				.OneToMany<UserRating, UserRate>(x => x.ratingList, x => x.UserRatingRatingListId)
				.OneToMany<UserRating, UserRate>(x => x.newRatingList, x => x.UserRatingNewRatingListId)
				.OneToMany<UserRating, UserRate>(x => x.nextRatingList, x => x.UserRatingNextRatingListId)
				.OneToMany<UserRating, UserRate>(x => x.nextNewRatingList, x => x.UserRatingNextNewRatingListId);
		}

		[JsonIgnore]
		public ulong UserDetailId { get; set; }

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