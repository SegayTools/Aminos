using System;
using System.Threading.Tasks;
using AminosUI.ViewModels.Pages;

namespace AminosUI.Services.Navigations;

public interface IApplicationNavigation
{
    ValueTask NavigatePageAsync<T>(T existObj = default) where T : PageViewModelBase;
    ValueTask NavigatePageAsync(Type pageViewModelType);
}