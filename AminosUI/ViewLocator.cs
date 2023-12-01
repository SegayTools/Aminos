using AminosUI.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AminosUI
{
	public class ViewLocator : IDataTemplate
	{
		public Control Build(object data)
		{
			var name = string.Join(".", data.GetType().FullName.Split(".").Select(x =>
			{
				if (x == "ViewModels")
					return "Views";
				if (x.Length > "ViewModel".Length && x.EndsWith("ViewModel"))
					return x.Substring(0, x.Length - "Model".Length);
				return x;
			}));
			var type = Type.GetType(name);

			if (type == null)
			{
				var msg = $"<viwe type not found:{name}; model type:{data.GetType().FullName}>";
#if DEBUG
				throw new Exception(msg);
#else
				return new TextBlock { Text = msg };
#endif
			}

			var control = (Control)ActivatorUtilities.CreateInstance((Application.Current as App).Service, type);
			control.DataContext = data;
			return control;
		}

		public bool Match(object data)
		{
			return data is ViewModelBase;
		}
	}
}
