using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AminosUI.ViewModels;

public class ViewModelBase : ObservableObject
{
	public virtual void OnViewAfterLoaded(Control control)
	{

	}

	public virtual void OnViewBeforeUnload(Control control)
	{

	}
}
