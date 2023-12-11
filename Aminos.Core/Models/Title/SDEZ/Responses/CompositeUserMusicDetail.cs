using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses;

public class CompositeUserMusicDetail
{
    public UserMusicDetail UserMusicDetail { get; set; }
    public UserDetail UserDetail { get; set; }
    
    public int Rank { get; set; }
}