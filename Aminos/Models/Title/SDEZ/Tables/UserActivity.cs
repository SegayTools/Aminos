using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserActivities")]
    public class UserActivity
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public UserAct[] playList;

        public UserAct[] musicList;
    }
}