using Aminos.Models.Title.SDEZ.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserMusicDetails")]
    public class UserMusicDetail
    {
        public UserDetail UserDetail { get; set; }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int musicId;

        public MusicDifficultyID level;

        public uint playCount;

        public uint achievement;

        public PlayComboflagID comboStatus;

        public PlaySyncflagID syncStatus;

        public uint deluxscoreMax;

        public MusicClearrankID scoreRank;

        public uint extNum1;
    }
}