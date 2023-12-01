using Aminos.Core.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Aminos.Databases.Title.SDEZ
{
	public class MaimaiDXDB : DbContext
	{
		private readonly ILogger logger;

		public MaimaiDXDB(ILoggerFactory loggerFactory, DbContextOptions<MaimaiDXDB> options) : base(options)
		{
			logger = loggerFactory.CreateLogger<MaimaiDXDB>();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
		}

		public DbSet<GameEvent> GameEvents { get; set; }
		public DbSet<GameRanking> GameRanks { get; set; }
		public DbSet<GameCharge> GameCharges { get; set; }

		public DbSet<ClientBookkeeping> ClientBookkeepings { get; set; }
		public DbSet<ClientSetting> ClientSettings { get; set; }
		public DbSet<ClientTestmode> ClientTestmodes { get; set; }

		public DbSet<UserDetail> UserDetails { get; set; }
		public DbSet<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }
	}
}
