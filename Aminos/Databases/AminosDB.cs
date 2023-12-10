using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Models.General.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aminos.Databases
{
	public class AminosDB : DbContext
	{
		public AminosDB(DbContextOptions<AminosDB> options) : base(options)
		{

		}

		public DbSet<Card> Cards { get; set; }
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<Keychip> Keychips { get; set; }
		public DbSet<Announcement> Announcements { get; set; }
		public DbSet<GameplayLog> GameplayLogs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
