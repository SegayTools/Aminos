using System;
using System.Threading.Tasks;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.ViewModels.Pages;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace AminosUI.Services.Navigations.DefaultImpl;

[RegisterInjectable(typeof(IApplicationNavigation))]
public class MainWindowApplicationNavigationProxy : IApplicationNavigation
{
    private readonly IApplicationNavigation actualNavigationImpl;

    public MainWindowApplicationNavigationProxy()
    {
        actualNavigationImpl = Application.Current.ApplicationLifetime switch
        {
            IClassicDesktopStyleApplicationLifetime desktopApp => desktopApp.MainWindow.DataContext,
            ISingleViewApplicationLifetime viewApp => viewApp.MainView.DataContext
        } as IApplicationNavigation;
    }

    public async ValueTask NavigatePageAsync<T>(T existObj = default) where T : PageViewModelBase
    {
        actualNavigationImpl?.NavigatePageAsync(existObj);
    }

    public async ValueTask NavigatePageAsync(Type pageViewModelType)
    {
        actualNavigationImpl?.NavigatePageAsync(pageViewModelType);
    }
}