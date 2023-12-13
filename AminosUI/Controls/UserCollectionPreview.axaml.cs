using Avalonia;
using Avalonia.Controls;

namespace AminosUI.Controls;

public partial class UserCollectionPreview : UserControl
{
    public static readonly AvaloniaProperty<int> IconIdProperty =
        AvaloniaProperty.Register<UserCollectionPreview, int>(nameof(IconId));

    public static readonly AvaloniaProperty<int> FrameIdProperty =
        AvaloniaProperty.Register<UserCollectionPreview, int>(nameof(FrameId));

    public static readonly AvaloniaProperty<int> PlateIdProperty =
        AvaloniaProperty.Register<UserCollectionPreview, int>(nameof(PlateId));

    public static readonly AvaloniaProperty<string> TitleStringProperty =
        AvaloniaProperty.Register<UserCollectionPreview, string>(nameof(TitleString), string.Empty);

    public static readonly AvaloniaProperty<string> TitleRareProperty =
        AvaloniaProperty.Register<UserCollectionPreview, string>(nameof(TitleRare), string.Empty);

    public static readonly AvaloniaProperty<string> UserNameProperty =
        AvaloniaProperty.Register<UserCollectionPreview, string>(nameof(UserName), string.Empty);

    public UserCollectionPreview()
    {
        InitializeComponent();
        container.DataContext = this;
    }

    public int IconId
    {
        get => (int) GetValue(IconIdProperty);
        set => SetValue(IconIdProperty, value);
    }

    public int FrameId
    {
        get => (int) GetValue(FrameIdProperty);
        set => SetValue(FrameIdProperty, value);
    }

    public int PlateId
    {
        get => (int) GetValue(PlateIdProperty);
        set => SetValue(PlateIdProperty, value);
    }

    public string UserName
    {
        get => (string) GetValue(UserNameProperty);
        set => SetValue(UserNameProperty, value);
    }

    public string TitleString
    {
        get => (string) GetValue(TitleStringProperty);
        set => SetValue(TitleStringProperty, value);
    }

    public string TitleRare
    {
        get => (string) GetValue(TitleRareProperty);
        set => SetValue(TitleRareProperty, value);
    }
}