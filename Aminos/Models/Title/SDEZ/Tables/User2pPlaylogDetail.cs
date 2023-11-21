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
	[Table("MaimaiDX_User2pPlaylogDetails")]
	[RegisterInjectable(typeof(IModelCreateBuilder<MaimaiDXDB>))]
	public class User2pPlaylogDetail : IModelCreateBuilder<MaimaiDXDB>
	{
		public void OnModelCreateBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.
				ManyToOne<User2pPlaylogDetail, UserDetail>(x => x.UserDetail1).
				ManyToOne<User2pPlaylogDetail, UserDetail>(x => x.UserDetail2);
		}

		[JsonIgnore]
		[Key]
		public int Id { set; get; }

		[JsonIgnore]
		public UserDetail UserDetail1 { get; set; }

		[JsonIgnore]
		public UserDetail UserDetail2 { get; set; }

		public int musicId { get; set; }

		public int level { get; set; }

		public int achievement { get; set; }

		public int deluxscore { get; set; }

		public string userPlayDate { get; set; }
	}
}