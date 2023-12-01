using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Models.General.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aminos.Databases
{
	public class AminosDB : DbContext
	{
		private readonly ILogger logger;

		public AminosDB(ILoggerFactory loggerFactory, DbContextOptions<AminosDB> options) : base(options)
		{
			logger = loggerFactory.CreateLogger<AminosDB>();
		}

		public DbSet<Card> Cards { get; set; }
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<Keychip> Keychips { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
