using System;
using Aminos.Core.Services.Injections;
using AminosUI.ViewModels;
using AminosUI.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Notification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AminosUI;

public class App : Application
{
    public IServiceProvider RootServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        InitServiceProvider();
        var mainViewModel = ActivatorUtilities.CreateInstance<MainViewModel>(RootServiceProvider);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };

        base.OnFrameworkInitializationCompleted();
    }

    private void InitServiceProvider()
    {
        if (RootServiceProvider is not null)
            throw new Exception("InitServiceProvider() has been called.");

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(o =>
        {
            o.AddDebug();
            o.SetMinimumLevel(LogLevel.Debug);
        });
        serviceCollection.AddHttpClient();
        serviceCollection.AddKeyedScoped<IServiceCollection>("AppBuild", (provider, o) =>
        {
            if (o is string key && key == "AppBuild")
                return serviceCollection;
            throw new Exception("Not allow get IServiceCollection objects.");
        });
        serviceCollection.AddInjectsByAttributes(typeof(App).Assembly);
        serviceCollection.AddSingleton<INotificationMessageManager, NotificationMessageManager>();

        RootServiceProvider = serviceCollection.BuildServiceProvider();
    }
}