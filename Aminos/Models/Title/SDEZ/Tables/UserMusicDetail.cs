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
	}
}