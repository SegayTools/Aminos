using System;
using System.Threading;
using System.Threading.Tasks;
using AminosUI.Services.Applications.User;
using AminosUI.Services.Navigations;
using AminosUI.Services.Notifications;
using AminosUI.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace AminosUI.ViewModels.Pages.User;

public partial class UserLoginPageViewModel : PageViewModelBase
{
    private readonly ILogger<UserLoginPageViewModel> logger;
    private readonly IApplicationNavigation navigation;
    private readonly IApplicationNotification notification;
    private readonly IUserManager userManager;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendTokenToEmailCommand))]
    private string email;

    [ObservableProperty]
    private UserLoginPageState pageState = UserLoginPageState.Login;

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
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string userName;

    public UserLoginPageViewModel()
    {
        DesignModeHelper.CheckOnlyForDesignMode();
    }

    public UserLoginPageViewModel(IApplicationNavigation navigation, IUserManager userManager,
        ILogger<UserLoginPageViewModel> logger, IApplicationNotification notification)
    {
        this.navigation = navigation;
        this.userManager = userManager;
        this.logger = logger;
        this.notification = notification;
    }

    public override string Title => "用户登录";

    [RelayCommand]
    private void SwitchPageState(string stateString)
    {
        SwitchPageState(Enum.Parse<UserLoginPageState>(stateString));
    }

    private void SwitchPageState(UserLoginPageState state)
    {
        PageState = state;
    }

    private bool CanLogin()
    {
        return !(string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password));
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login(CancellationToken cancellationToken)
    {
        using var d = notification.BeginLoadingNotification("登录请求中...", out var cancellation);

        var result = await userManager.Login(UserName, Password, cancellation);
        d.Dispose();

        if (result.isSuccess)
        {
            notification.ShowInfomation($"登录成功！您好，{userManager.CurrentUser.Name}");
            await navigation.NavigatePageAsync<UserInfoPageViewModel>();
        }
        else
        {
            notification.ShowError($"登录失败！原因: {result.message}");
        }
    }

    private bool CanRegister()
    {
        return Repassword == Password &&
               !(
                   string.IsNullOrWhiteSpace(UserName) ||
                   string.IsNullOrWhiteSpace(Password) ||
                   string.IsNullOrWhiteSpace(Repassword) ||
                   string.IsNullOrWhiteSpace(Email)
               );
    }

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task Register(CancellationToken cancellationToken)
    {
        using var d = notification.BeginLoadingNotification("注册请求中...", out var cancellation);

        var result = await userManager.Register(UserName, Password, Email, cancellation);
        d.Dispose();

        if (result.isSuccess)
        {
            notification.ShowInfomation("注册成功!");
            Password = Repassword = default;
            SwitchPageState(UserLoginPageState.Login);
        }
        else
        {
            notification.ShowError($"注册失败！原因: {result.message}");
        }
    }

    private bool CanResetPassword()
    {
        return Repassword == Password &&
               !(
                   string.IsNullOrWhiteSpace(Password) ||
                   string.IsNullOrWhiteSpace(Token) ||
                   string.IsNullOrWhiteSpace(Repassword)
               );
    }

    [RelayCommand(CanExecute = nameof(CanResetPassword))]
    private async Task ResetPassword(CancellationToken cancellationToken)
    {
        using var d = notification.BeginLoadingNotification("修改密码请求中...", out var cancellation);

        var result = await userManager.ResetPassword(Password, cancellation);
        d.Dispose();

        if (result.isSuccess)
        {
            notification.ShowInfomation("修改成功!");
            Password = Repassword = default;
            SwitchPageState(UserLoginPageState.Login);
        }
        else
        {
            notification.ShowError($"修改失败！原因: {result.message}");
        }
    }

    private bool CanSendTokenToEmail()
    {
        return !string.IsNullOrWhiteSpace(Email);
    }

    [RelayCommand(CanExecute = nameof(CanSendTokenToEmail))]
    private async Task SendTokenToEmail(CancellationToken cancellationToken)
    {
        using var d = notification.BeginLoadingNotification("发送Token请求中...", out var cancellation);

        var result = await userManager.SendToken(Email, cancellation);
        d.Dispose();

        if (result.isSuccess)
            notification.ShowInfomation("发送成功!请去邮箱查看邮件拿到Token.");
        else
            notification.ShowError($"发送失败！原因: {result.message}");
    }
}