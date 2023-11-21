using Aminos.Kernels.Injections.Attrbutes;
using System.Reflection;

namespace Aminos.Kernels.Injections
{
	public static class AddInjectAttbuteServiceCollection
	{
		public static IServiceCollection AddInjectsByAttributes(this IServiceCollection services, Assembly assembly)
		{
			var types = assembly.GetTypes().Where(type => type.GetCustomAttributes<RegisterInjectableAttribute>().Any());

			foreach (var type in types)
			{
				foreach (var attr in type.GetCustomAttributes<RegisterInjectableAttribute>())
				{
					switch (attr.ServiceLifetime)
					{
						case ServiceLifetime.Singleton:
							services.AddSingleton(attr.TargetInjectType, type);
							break;
						case ServiceLifetime.Scoped:
							services.AddScoped(attr.TargetInjectType, type);
							break;
						case ServiceLifetime.Transient:
							services.AddTransient(attr.TargetInjectType, type);
							break;
						default:
							break;
					}
				}
			}

			return services;
		}
	}
}
