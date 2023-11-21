using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Databases.Title.SDEZ
{
    public class MaimaiDXDB : DbContext
	{
		public MaimaiDXDB(DbContextOptions<MaimaiDXDB> options) : base(options)
		{

		}

		public DbSet<GameEvent> GameEvents { get; set; }
		public DbSet<GameRanking> GameRanks { get; set; }
		public DbSet<GameCharge> GameCharges { get; set; }
		public DbSet<UserDetail> UserDetails { get; set; }
		public DbSet<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }
	}
}
