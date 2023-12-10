using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using MessagePack;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[MessagePackObject]
	public class UserUdemae
	{

		[Key(0)]
		public int rate { get; set; }

		[Key(1)]
		public int maxRate { get; set; }

		[Key(2)]
		public int classValue { get; set; }

		[Key(3)]
		public int maxClassValue { get; set; }

		[Key(4)]
		public uint totalWinNum { get; set; }

		[Key(5)]
		public uint totalLoseNum { get; set; }

		[Key(6)]
		public uint maxWinNum { get; set; }

		[Key(7)]
		public uint maxLoseNum { get; set; }

		[Key(8)]
		public uint winNum { get; set; }

		[Key(9)]
		public uint loseNum { get; set; }

		[Key(10)]
		public uint npcTotalWinNum { get; set; }

		[Key(11)]
		public uint npcTotalLoseNum { get; set; }

		[Key(12)]
		public uint npcMaxWinNum { get; set; }

		[Key(13)]
		public uint npcMaxLoseNum { get; set; }

		[Key(14)]
		public uint npcWinNum { get; set; }

		[Key(15)]
		public uint npcLoseNum { get; set; }
	}
}