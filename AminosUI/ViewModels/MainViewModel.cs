using AminosUI.Controls.ControlTemplates;
using AminosUI.Services.Notifications;
using AminosUI.Services.ViewModelFactory;
using AminosUI.ViewModels.Pages.User;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Notification;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AminosUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
	private readonly IViewModelFactory viewModelFactory;

	private readonly IApplicationNotification notification;
	public INotificationMessageManager MessageManager => notification.MessageManager;

	[ObservableProperty]
	private bool isPaneOpen;

	[ObservableProperty]
	private ViewModelBase mainPageContent;

	[ObservableProperty]
	private ListItemTemplate selectedListItem;

	public ObservableCollection<ListItemTemplate> TopItems { get; } = new()
	{
		new ListItemTemplate(typeof(MainViewModel), "主页"),
		new ListItemTemplate(typeof(MainViewModel), "用户信息"),
		new ListItemTemplate(typeof(MainViewModel), "maimai DX"),
	};

	public ObservableCollection<ListItemTemplate> BottomItems { get; } = new()
	{

	};

	public MainViewModel(IViewModelFactory viewModelFactory, IApplicationNotification notification)
	{
		this.viewModelFactory = viewModelFactory;
		this.notification = notification;
		MainPageContent = viewModelFactory.CreateViewModel<UserLoginPageViewModel>();
	}

	partial void OnSelectedListItemChanged(ListItemTemplate oldValue, ListItemTemplate newValue)
	{
		MainPageContent = viewModelFactory.CreateViewModel(newValue.ModelType);
	}

	[RelayCommand]
	private void TriggerPane()
	{
		IsPaneOpen = !IsPaneOpen;
	}
}
