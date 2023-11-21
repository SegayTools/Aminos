using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_User2pPlaylogDetails")]
    public class User2pPlaylogDetail
    {
        [JsonIgnore]
        [Key]
        public int Id { set; get; }

        [JsonIgnore]
        public UserDetail UserDetail1 { get; set; }

        [JsonIgnore]
        public UserDetail UserDetail2 { get; set; }

        public int musicId;

        public int level;

        public int achievement;

        public int deluxscore;

        public string userPlayDate;
    }
}