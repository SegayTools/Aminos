using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_MusicDatas")]
public class MusicData
{
    [Key]
    public int Id { get; set; }

    public int AssetId => Id % 10000;

    public string Name { get; set; }
    public string Artist { get; set; }
    public string RightsInfoName { get; set; }
    public string ReleaseTagName { get; set; }
    public string SortName { get; set; }
    public string GenreName { get; set; }
    public float Bpm { get; set; }
    public int Version { get; set; }
    public string AddVersion { get; set; }
    public bool Dresscode { get; set; }

    public virtual EventData EventName { get; set; }
    public virtual EventData EventName2 { get; set; }
    public virtual EventData SubEventName { get; set; }

    public int LockType { get; set; }
    public int SubLockType { get; set; }
    public bool DotNetListView { get; set; }
    public string UtageKanjiName { get; set; }
    public string Comment { get; set; }
    public int UtagePlayStyle { get; set; }
    public int Priority { get; set; }

    [Column(nameof(NotesData))]
    [MaxLength(2048)]
    [JsonIgnore]
    public string __notesData { get; set; }

    [NotMapped]
    [JsonInclude]
    public NotesDataCollection NotesData
    {
        get => __notesData is null ? default : JsonSerializer.Deserialize<NotesDataCollection>(__notesData);
        set => __notesData = JsonSerializer.Serialize(value);
    }

    [Column(nameof(FixedOptions))]
    [MaxLength(2048)]
    [JsonIgnore]
    public string __fixedOptions { get; set; }

    [NotMapped]
    [JsonInclude]
    public FixedOptionCollection FixedOptions
    {
        get => __fixedOptions is null ? default : JsonSerializer.Deserialize<FixedOptionCollection>(__fixedOptions);
        set => __fixedOptions = JsonSerializer.Serialize(value);
    }

    public bool IsDeluxe => Id / 10000 % 10 == 1;

    public record Note(string FilePath, int Level, int LevelDecimal, string designer, int maxNotes);

    public class NotesDataCollection
    {
        public List<Note> Notes { get; set; } = new();
    }

    public record FixedOption(string FixedOptionName, string FixedOptionValue);

    public class FixedOptionCollection
    {
        public List<FixedOption> FixedOptions { get; set; } = new();
    }
}