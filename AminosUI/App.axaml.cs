using Aminos.Core.Services.Injections;
using AminosUI.ViewModels;
using AminosUI.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Notification;
using Avalonia.Notification.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AminosUI;

public partial class App : Application
{
	private IServiceProvider serviceProvider;
	public IServiceProvider Service => serviceProvider;

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		InitServiceProvider();

		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = ActivatorUtilities.CreateInstance<MainViewModel>(Service)
			};
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			singleViewPlatform.MainView = new MainView
			{
				DataContext = ActivatorUtilities.CreateInstance<MainViewModel>(Service)
			};
		}

		base.OnFrameworkInitializationCompleted();
	}

	private void InitServiceProvider()
	{
		if (Service is not null)
			throw new Exception("InitServiceProvider() has been called.");

		var serviceCollection = new ServiceCollection();

		serviceCollection.AddLogging(o =>
		{
			o.AddDebug();
			o.SetMinimumLevel(LogLevel.Debug);
		});
		serviceCollection.AddHttpClient();
		serviceCollection.AddInjectsByAttributes(typeof(App).Assembly);
		serviceCollection.AddSingleton<INotificationMessageManager, NotificationMessageManager>();

		serviceProvider = serviceCollection.BuildServiceProvider();
	}
}
