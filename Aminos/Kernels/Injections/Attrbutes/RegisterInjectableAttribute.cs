namespace Aminos.Kernels.Injections.Attrbutes
{
	public class RegisterInjectableAttribute : Attribute
	{
		private readonly Type type;

		public RegisterInjectableAttribute(Type targetType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			this.type = targetType;
			ServiceLifetime = serviceLifetime;
		}

		public Type TargetInjectType => type;

		public ServiceLifetime ServiceLifetime { get; }
	}
}
