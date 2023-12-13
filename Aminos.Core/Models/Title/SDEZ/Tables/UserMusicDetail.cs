using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_UserMusicDetails")]
public class UserMusicDetail
{
    [JsonIgnore]
    public virtual UserDetail UserDetail { get; set; }

    [Key]
    [JsonIgnore]
    public ulong Id { get; set; }

    public int musicId { get; set; }

    public MusicDifficultyID level { get; set; }

    public uint playCount { get; set; }

    public uint achievement { get; set; }

    public PlayComboflagID comboStatus { get; set; }

    public PlaySyncflagID syncStatus { get; set; }

    public uint deluxscoreMax { get; set; }

    public MusicClearrankID scoreRank { get; set; }

    public uint extNum1 { get; set; }

    public string AchievementPresentDisplay => $"{achievement / 10000.0f:F4}%";
}