using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace AminosUI.Utils.MethodExtensions;

public static class AppBuilderMethodExtensions
{
    public static AppBuilder AppendDependencyInject(this AppBuilder builer, Action<IServiceCollection> injectConfigFunc)
    {
        AppBuilderStatic.injectConfigFunc += injectConfigFunc;
        return builer;
    }

    internal class AppBuilderStatic
    {
        internal static Action<IServiceCollection> injectConfigFunc = serviceCollection => { };
    }
}