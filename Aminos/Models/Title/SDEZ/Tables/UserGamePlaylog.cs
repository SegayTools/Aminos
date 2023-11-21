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
		public ulong Id { get; set; }

		public string version { get; set; }

		public string playDate { get; set; }

		public int playMode { get; set; }

		public int useTicketId { get; set; }

		public int playCredit { get; set; }

		public int playTrack { get; set; }

		public string clientId { get; set; }

		public bool isPlayTutorial { get; set; }

		public bool isEventMode { get; set; }

		public bool isNewFree { get; set; }

		public int playCount { get; set; }

		public int playSpecial { get; set; }

		public ulong playOtherUserId { get; set; }
	}
}