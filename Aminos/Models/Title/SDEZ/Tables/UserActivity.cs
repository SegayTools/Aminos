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
	[Table("MaimaiDX_UserActivities")]
	[RegisterInjectable(typeof(IModelCreateBuilder<MaimaiDXDB>))]
	public class UserActivity : IModelCreateBuilder<MaimaiDXDB>
	{
		public void OnModelCreateBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.OneToMany<UserActivity, UserAct>(x => x.playList, x => x.UserActivityPlayListId);
			modelBuilder.OneToMany<UserActivity, UserAct>(x => x.musicList, x => x.UserActivityMusicListId);
		}

		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public ICollection<UserAct> playList { get; set; } = new List<UserAct>();
		public ICollection<UserAct> musicList { get; set; } = new List<UserAct>();
	}
}