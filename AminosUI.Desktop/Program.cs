using System;
using Aminos.Core.Services.Injections;
using AminosUI.Utils.MethodExtensions;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Svg.Skia;

namespace AminosUI.Desktop;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        GC.KeepAlive(typeof(SvgImageExtension).Assembly);
        GC.KeepAlive(typeof(Avalonia.Svg.Skia.Svg).Assembly);

        var builder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .AppendDependencyInject(collection => collection.AddInjectsByAttributes(typeof(Program).Assembly))
            .LogToTrace(LogEventLevel.Warning);
        return builder;
    }
}