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
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
        [JsonIgnore]
        public ulong Id { get; set; }

        public int courseId { get; set; }

		public bool isLastClear { get; set; }

		public uint totalRestlife { get; set; }

		public uint totalAchievement { get; set; }

		public uint totalDeluxscore { get; set; }

		public uint playCount { get; set; }

		public string clearDate { get; set; }

		public string lastPlayDate { get; set; }

		public uint bestAchievement { get; set; }

		public string bestAchievementDate { get; set; }

		public uint bestDeluxscore { get; set; }

		public string bestDeluxscoreDate { get; set; }
	}
}