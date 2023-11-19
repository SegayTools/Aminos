using Aminos.Models.General;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Databases
{
	public class AminosDB : DbContext
	{
		public AminosDB(DbContextOptions<AminosDB> options) : base(options)
		{

		}

		public DbSet<Card> Cards { get; set; }
	}
}
