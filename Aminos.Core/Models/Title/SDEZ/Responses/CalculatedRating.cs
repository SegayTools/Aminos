using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses;

public class CalculatedRating
{
    public MusicData MusicData { get; set; } = new()
    {
        Id = 1453,
        Name = "OWEIGHIGESD",
        NotesData = new MusicData.NotesDataCollection
        {
            Notes = new List<MusicData.Note>
            {
                new(default, 13, 48, default, 130, true)
            }
        }
    };

    public UserMusicDetail MusicDetail { get; set; } = new()
    {
        achievement = 1007546,
        comboStatus = PlayComboflagID.Gold,
        syncStatus = PlaySyncflagID.SyncLow,
        level = MusicDifficultyID.Basic
    };

    public float RatingValue { get; set; } = 300;

    public float LevelDiff
    {
        get
        {
            var note = MusicData.NotesData.Notes.ElementAtOrDefault((int) MusicDetail.level);
            if (note is null)
                return default;
            return note.Level + note.LevelDecimal / 100.0f;
        }
    }
}