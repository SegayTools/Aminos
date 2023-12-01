using Aminos.Core.Models.Title.SDEZ.Enums;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	public class UserGhost
	{
		public string name { get; set; }

		public int iconId { get; set; }

		public int plateId { get; set; }

		public int titleId { get; set; }

		public int rate { get; set; }

		public int udemaeRate { get; set; }

		public uint courseRank { get; set; }

		public uint classRank { get; set; }

		public int classValue { get; set; }

		public string playDatetime { get; set; }

		public uint shopId { get; set; }

		public int regionCode { get; set; }

		public MusicDifficultyID typeId { get; set; }

		public int musicId { get; set; }

		public int difficulty { get; set; }

		public int version { get; set; }

		public byte[] resultBitList { get; set; }

		public int resultNum { get; set; }

		public int achievement { get; set; }
	}
}