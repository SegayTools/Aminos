using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Tables;
using AminosUI.Controls;

namespace AminosUI.ViewModels.Pages.MaimaiDx.Model;

public class MusicDisplayItem
{
    public MusicData MusicData { get; set; }
    public UserMusicDetail UserMusicDetail { get; set; }
    public MusicData.Note Note { get; set; }
    public MusicDisplayItemView.DisplayDiffType DifficultyId { get; set; }
    
    public MapBoundMusicData[] MapBoundMusicData { get; set; }
}