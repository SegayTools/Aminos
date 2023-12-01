using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Services.ViewModelFactory.DefaultImpl
{
	[RegisterInjectable(typeof(IViewModelFactory))]
	internal class DefaultViewModelFactory : IViewModelFactory
	{
		private readonly IServiceProvider serviceProvider;

		public DefaultViewModelFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public T CreateViewModel<T>() where T : ViewModelBase
		{
			return (T)CreateViewModel(typeof(T));
		}

		public ViewModelBase CreateViewModel(Type viewModelType)
		{
			return (ViewModelBase)ActivatorUtilities.CreateInstance(serviceProvider, viewModelType);
		}
	}
}
