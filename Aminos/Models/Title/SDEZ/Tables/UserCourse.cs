using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCourses")]
    public class UserCourse
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int courseId;

        public bool isLastClear;

        public uint totalRestlife;

        public uint totalAchievement;

        public uint totalDeluxscore;

        public uint playCount;

        public string clearDate;

        public string lastPlayDate;

        public uint bestAchievement;

        public string bestAchievementDate;

        public uint bestDeluxscore;

        public string bestDeluxscoreDate;
    }
}