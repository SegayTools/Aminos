using Microsoft.EntityFrameworkCore;

namespace Aminos.Kernels.Databases
{
	public interface IModelCreateBuilder<DBContextType> where DBContextType : DbContext
	{
		void OnModelCreateBuilder(ModelBuilder modelBuilder);
	}
}
