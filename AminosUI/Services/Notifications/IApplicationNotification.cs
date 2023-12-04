using Avalonia.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.Services.Notifications
{
	public interface IApplicationNotification
	{
		INotificationMessageManager MessageManager { get; }
		IDisposable BeginLoadingNotification(string message, out CancellationToken cancellationToken);

		void ShowInfomation(string message);
		void ShowWarnning(string message);
		void ShowError(Exception e, string message);
		void ShowError(string message);
	}
}
