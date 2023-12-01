using AminosUI.Services.Applications.User;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Notifications;
using Avalonia.Layout;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Notification;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.ViewModels.Pages.User
{
	public partial class UserLoginPageViewModel : ViewModelBase
	{
		private readonly IUserManager userManager;
		private readonly ILogger<UserLoginPageViewModel> logger;
		private readonly IApplicationNotification notification;

		[ObservableProperty]
		private UserLoginPageState pageState = UserLoginPageState.Login;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(LoginCommand))]
		private string userName;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(LoginCommand))]
		[NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
		[NotifyCanExecuteChangedFor(nameof(ResetPasswordCommand))]
		private string password;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
		[NotifyCanExecuteChangedFor(nameof(ResetPasswordCommand))]
		private string repassword;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(ResetPasswordCommand))]
		private string token;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(SendTokenToEmailCommand))]
		private string email;

		public UserLoginPageViewModel() => DesignModeHelper.CheckOnlyForDesignMode();

		public UserLoginPageViewModel(IUserManager userManager, ILogger<UserLoginPageViewModel> logger, IApplicationNotification notification)
		{
			this.userManager = userManager;
			this.logger = logger;
			this.notification = notification;
		}

		[RelayCommand]
		private void SwitchPageState(string stateString)
		{
			PageState = Enum.Parse<UserLoginPageState>(stateString);
		}

		private bool CanLogin() => !(string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password));
		[RelayCommand(CanExecute = nameof(CanLogin))]
		private async Task Login(CancellationToken cancellationToken)
		{

		}

		private bool CanRegister() =>
			Repassword == Password &&
			!(
			string.IsNullOrWhiteSpace(UserName) ||
			string.IsNullOrWhiteSpace(Password) ||
			string.IsNullOrWhiteSpace(Repassword) ||
			string.IsNullOrWhiteSpace(Email)
			);
		[RelayCommand(CanExecute = nameof(CanRegister))]
		private async Task Register(CancellationToken cancellationToken)
		{

		}

		private bool CanResetPassword() =>
			Repassword == Password &&
			!(
			string.IsNullOrWhiteSpace(Password) ||
			string.IsNullOrWhiteSpace(Token) ||
			string.IsNullOrWhiteSpace(Repassword)
			);
		[RelayCommand(CanExecute = nameof(CanResetPassword))]
		private async Task ResetPassword(CancellationToken cancellationToken)
		{

		}

		private bool CanSendTokenToEmail() => !(string.IsNullOrWhiteSpace(Email));
		[RelayCommand(CanExecute = nameof(CanSendTokenToEmail))]
		private async Task SendTokenToEmail(CancellationToken cancellationToken)
		{

		}
	}
}
