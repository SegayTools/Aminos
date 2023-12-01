using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aminos.Core.Utils.MethodExtensions
{
	public static class ModelBuilderMethodExtensions
	{
		public static ModelBuilder OneToMany<TEntity, TRelatedEntity>(this ModelBuilder builder, Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> d, Expression<Func<TRelatedEntity, object>> foreignKeyFunc = default) where TEntity : class where TRelatedEntity : class
		{
			var r = builder.Entity<TEntity>()
				.HasMany(d)
				.WithOne();
			if (foreignKeyFunc != null)
				r = r.HasForeignKey(foreignKeyFunc);
			r.IsRequired();
			return builder;
		}

		public static ModelBuilder ManyToOne<TEntity, TRelatedEntity>(this ModelBuilder builder, Expression<Func<TEntity, TRelatedEntity>> d) where TEntity : class where TRelatedEntity : class
		{
			builder.Entity<TEntity>()
				.HasOne(d)
				.WithMany()
				.IsRequired();
			return builder;
		}

		public static ModelBuilder OneToOne<TEntity, TRelatedEntity>(this ModelBuilder builder, Expression<Func<TEntity, TRelatedEntity>> d, Expression<Func<TRelatedEntity, object>> foreignKeyFunc = default) where TEntity : class where TRelatedEntity : class
		{
			var r = builder.Entity<TEntity>()
				.HasOne(d)
				.WithOne();
			if (foreignKeyFunc != null)
				r = r.HasForeignKey(foreignKeyFunc);
			r.IsRequired();
			return builder;
		}
	}
}
