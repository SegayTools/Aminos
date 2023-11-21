using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserUdemaes")]
	public class UserUdemae
	{
		[JsonIgnore]
		public ulong UserRatingId { get; set; }

		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int rate { get; set; }

		public int maxRate { get; set; }

		public int classValue { get; set; }

		public int maxClassValue { get; set; }

		public uint totalWinNum { get; set; }

		public uint totalLoseNum { get; set; }

		public uint maxWinNum { get; set; }

		public uint maxLoseNum { get; set; }

		public uint winNum { get; set; }

		public uint loseNum { get; set; }

		public uint npcTotalWinNum { get; set; }

		public uint npcTotalLoseNum { get; set; }

		public uint npcMaxWinNum { get; set; }

		public uint npcMaxLoseNum { get; set; }

		public uint npcWinNum { get; set; }

		public uint npcLoseNum { get; set; }
	}
}