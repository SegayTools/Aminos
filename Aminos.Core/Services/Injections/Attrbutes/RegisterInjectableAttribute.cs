using Microsoft.Extensions.DependencyInjection;

namespace Aminos.Core.Services.Injections.Attrbutes
{
    public class RegisterInjectableAttribute : Attribute
    {
        private readonly Type type;

        public RegisterInjectableAttribute(Type targetType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            type = targetType;
            ServiceLifetime = serviceLifetime;
        }

        public Type TargetInjectType => type;

        public ServiceLifetime ServiceLifetime { get; }
    }
}
