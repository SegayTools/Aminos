using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aminos.Databases.Title.SDEZ
{
	public class MaimaiDXDB : DbContext
	{
		public MaimaiDXDB(DbContextOptions<MaimaiDXDB> options, IServiceProvider serviceProvider) : base(options)
		{
			var tracker = ChangeTracker;
		}

		public DbSet<GameEvent> GameEvents { get; set; }
		public DbSet<GameRanking> GameRanks { get; set; }
		public DbSet<GameCharge> GameCharges { get; set; }
		public DbSet<UserDetail> UserDetails { get; set; }
		public DbSet<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
