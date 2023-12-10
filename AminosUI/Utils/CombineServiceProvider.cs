using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Utils
{
	public class CombineServiceProvider : IServiceProvider
	{
		private readonly IServiceProvider[] services;

		public CombineServiceProvider(params IServiceProvider[] services)
		{
			this.services = services;
		}

		public object GetService(Type serviceType)
		{
			//SUPER GENUIS I AM
			if (serviceType == typeof(IServiceProvider))
				return this;

			foreach (var service in services)
			{
				var val = service.GetService(serviceType);
				if (val != default) return val;
			}

			return default;
		}
	}
}
