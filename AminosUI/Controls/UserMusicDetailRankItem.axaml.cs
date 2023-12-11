using Aminos.Core.Models.Title.SDEZ.Tables;
using Avalonia;
using Avalonia.Controls;

namespace AminosUI.Controls;

public partial class UserMusicDetailRankItem : UserControl
{
    public static StyledProperty<UserMusicDetail> UserMusicDetailProperty =
        AvaloniaProperty.Register<UserMusicDetailRankItem, UserMusicDetail>(nameof(UserMusicDetail),
            default, true);

    public static StyledProperty<int> RankProperty =
        AvaloniaProperty.Register<UserMusicDetailRankItem, int>(nameof(Rank),
            0, true);
    
    public static StyledProperty<UserDetail> UserDetailProperty =
        AvaloniaProperty.Register<UserMusicDetailRankItem, UserDetail>(nameof(UserDetail),
            default, true);
    
    public static StyledProperty<MusicData> MusicDataProperty =
        AvaloniaProperty.Register<UserMusicDetailRankItem, MusicData>(nameof(MusicData),
            default, true);

    public UserMusicDetailRankItem()
    {
        InitializeComponent();
        container.DataContext = this;
    }

    public UserMusicDetail UserMusicDetail
    {
        get => GetValue(UserMusicDetailProperty);
        set => SetValue(UserMusicDetailProperty, value);
    }
    
    public MusicData MusicData
    {
        get => GetValue(MusicDataProperty);
        set => SetValue(MusicDataProperty, value);
    }

    public int Rank
    {
        get => GetValue(RankProperty);
        set => SetValue(RankProperty, value);
    }
    
    public UserDetail UserDetail
    {
        get => GetValue(UserDetailProperty);
        set => SetValue(UserDetailProperty, value);
    }
}