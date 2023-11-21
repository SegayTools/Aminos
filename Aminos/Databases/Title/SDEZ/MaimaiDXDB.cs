using Aminos.Kernels.Databases;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aminos.Databases.Title.SDEZ
{
	public class MaimaiDXDB : DbContext
	{
		private readonly IEnumerable<IModelCreateBuilder<MaimaiDXDB>> builders;

		public MaimaiDXDB(DbContextOptions<MaimaiDXDB> options, IServiceProvider serviceProvider) : base(options)
		{
			builders = serviceProvider.GetServices<IModelCreateBuilder<MaimaiDXDB>>();
		}

		public DbSet<GameEvent> GameEvents { get; set; }
		public DbSet<GameRanking> GameRanks { get; set; }
		public DbSet<GameCharge> GameCharges { get; set; }
		public DbSet<UserDetail> UserDetails { get; set; }
		public DbSet<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var builder in builders)
				builder.OnModelCreateBuilder(modelBuilder);
		}
	}
}
