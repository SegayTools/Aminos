using Aminos.Core.Utils.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserCourses")]
	public class UserCourse
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int courseId { get; set; }

		public bool isLastClear { get; set; }

		public uint totalRestlife { get; set; }

		public uint totalAchievement { get; set; }

		public uint totalDeluxscore { get; set; }

		public uint playCount { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime clearDate { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime lastPlayDate { get; set; }

		public uint bestAchievement { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime bestAchievementDate { get; set; }

		public uint bestDeluxscore { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime bestDeluxscoreDate { get; set; }
	}
}