using Aminos.Core.Services.Injections.Attrbutes;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;
using Avalonia.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AminosUI.Utils;
using System.Threading;

namespace AminosUI.Services.Notifications.DefaultImpl
{
	[RegisterInjectable(typeof(IApplicationNotification))]
	internal class DefaultApplicationNotification : IApplicationNotification
	{
		private readonly INotificationMessageManager notificationMessageManager;

		public INotificationMessageManager MessageManager => notificationMessageManager;

		public DefaultApplicationNotification(INotificationMessageManager notificationMessageManager = null)
		{
			this.notificationMessageManager = notificationMessageManager ?? new NotificationMessageManager();
		}

		public void ShowError(Exception e, string message)
		{
			notificationMessageManager
				.CreateMessage()
				.HasMessage(message)
				.HasHeader("Error")
				.Foreground("White")
				.Background("#FF6A6A")
				.Animates(true)
				.Dismiss().WithButton("关闭", (msg) => { })
				.Queue();
		}

		public void ShowInfomation(string message)
		{
			notificationMessageManager
				.CreateMessage()
				.HasMessage(message)
				.HasHeader("Info")
				.Foreground("White")
				.Background("#4EEE94")
				.Animates(true)
				.Dismiss().WithDelay(TimeSpan.FromSeconds(2))
				.Queue();
		}

		public IDisposable ShowLoadingNotification(string message, out CancellationToken cancellationToken)
		{
			var cancellationSource = new CancellationTokenSource();
			cancellationToken = cancellationSource.Token;

			var msg = notificationMessageManager
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
				.Dismiss().WithButton("取消执行", (msg) =>
				{
					cancellationSource.Cancel();
				})
				.Queue();

			return new DisposeCallbackInvoker(() =>
			{
				MessageManager.Dismiss(msg);
			});
		}

		public void ShowWarnning(string message)
		{
			notificationMessageManager
				.CreateMessage()
				.HasMessage(message)
				.HasHeader("Warn")
				.Foreground("White")
				.Background("#EEC900")
				.Animates(true)
				.Dismiss().WithDelay(TimeSpan.FromSeconds(3))
				.Queue();
		}
	}
}
