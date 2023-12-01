using Aminos.Core.Utils.Json;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ
{
	public class UserPortrait
	{
		public ulong userId { get; set; }

		public int divNumber { get; set; }

		public int divLength { get; set; }

		public string divData { get; set; }

		public int placeId { get; set; }

		public string clientId { get; set; }

		[JsonConverter(typeof(TitleString2DateTimeConverter))]
		public DateTime uploadDate { get; set; }

		public string fileName { get; set; }
	}
}