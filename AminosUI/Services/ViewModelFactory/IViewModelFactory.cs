using AminosUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Services.ViewModelFactory
{
	public interface IViewModelFactory
	{
		public T CreateViewModel<T>() where T : ViewModelBase;
		public ViewModelBase CreateViewModel(Type viewModelType);
	}
}
