using System;
using System.Threading;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace AminosUI.Services.Notifications.DefaultImpl;

[RegisterInjectable(typeof(IApplicationNotification), ServiceLifetime.Singleton)]
internal class DefaultApplicationNotification : IApplicationNotification
{
    public DefaultApplicationNotification(INotificationMessageManager notificationMessageManager = null)
    {
        MessageManager = notificationMessageManager ?? new NotificationMessageManager();
    }

    public INotificationMessageManager MessageManager { get; }

    public void ShowInfomation(string message)
    {
        MessageManager
            .CreateMessage()
            .HasMessage(message)
            .HasHeader("Info")
            .Foreground("White")
            .Background("#4EEE94")
            .Animates(true)
            .Dismiss().WithDelay(TimeSpan.FromSeconds(2))
            .Queue();
    }

    public IDisposable BeginLoadingNotification(string message, out CancellationToken cancellationToken)
    {
        var cancellationSource = new CancellationTokenSource();
        cancellationToken = cancellationSource.Token;

        var msg = MessageManager
            .CreateMessage()
            .HasMessage(message)
            .HasHeader("Processing...")
            .Foreground("White")
            .Background("#00BFFF")
            .Animates(true)
            .WithOverlay(new ProgressBar
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 3,
                BorderThickness = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)),
                Background = Brushes.Transparent,
                IsIndeterminate = true,
                IsHitTestVisible = false
            })
            .Dismiss().WithButton("取消执行", msg => { cancellationSource.Cancel(); })
            .Queue();

        return new DisposeCallbackInvoker(() => { MessageManager.Dismiss(msg); }, true);
    }

    public void ShowWarnning(string message)
    {
        MessageManager
            .CreateMessage()
            .HasMessage(message)
            .HasHeader("Warn")
            .Foreground("White")
            .Background("#EEC900")
            .Animates(true)
            .Dismiss().WithDelay(TimeSpan.FromSeconds(3))
            .Queue();
    }

    public void ShowError(string message)
    {
        MessageManager
            .CreateMessage()
            .HasMessage(message)
            .HasHeader("Error")
            .Foreground("White")
            .Background("#FF6A6A")
            .Animates(true)
            .Dismiss().WithButton("关闭", msg => { })
            .Queue();
    }

    public void ShowError(Exception e, string message)
    {
        ShowError($"{message}, 异常信息:{e.Message}");
    }
}