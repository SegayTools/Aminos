using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserGamePlaylogs")]
    public class UserGamePlaylog
    {
        [JsonPropertyName("playlogId")]
        public int Id { get; set; }

        public string version;

        public string playDate;

        public int playMode;

        public int useTicketId;

        public int playCredit;

        public int playTrack;

        public string clientId;

        public bool isPlayTutorial;

        public bool isEventMode;

        public bool isNewFree;

        public int playCount;

        public int playSpecial;

        public ulong playOtherUserId;
    }
}