using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aminos.Utils.MethodExtensions
{
	public static class DBContextMethodExtensions
	{
		public static Action<object, object> GenerateCopier<T>(this DbContext dbContext)
		{
			var entityType = dbContext.Model.FindEntityType(typeof(T));
			var keyNames = entityType.FindPrimaryKey().Properties.Select(x => x.Name);

			// 获取类型的所有属性
			var properties = entityType.ClrType.GetProperties()
									  .Where(p => !keyNames.Contains(p.Name))// 排除键的属性
									  .Where(x => x.CanWrite && x.CanRead);

			var setter = new Action<object, object>((a, b) => { });

			foreach (var property in properties)
			{
				setter += (entity, copy) =>
				{
					var value = property.GetValue(copy);
					property.SetValue(entity, value);
				};
			}

			return setter;
		}

		private static Dictionary<(Type, Type), Action<object, object>> CachedCopierFuncMap = new();

		public static void CopyValuesWithoutKeys<T>(this DbContext dbContext, T entityTarget, T copySource) where T : class
		{
			var contextType = dbContext.GetType();
			var entityType = entityTarget.GetType();

			var key = (contextType, entityType);
			if (!CachedCopierFuncMap.TryGetValue(key, out var copier))
				copier = CachedCopierFuncMap[key] = GenerateCopier<T>(dbContext);

			copier(entityTarget, copySource);
		}
	}
}
