using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Responses
{
    public abstract class PowerOnResponseBase : QueryPathSerializeBase
	{
		public int stat { get; set; } = 1;
		public string uri { get; set; }
		public string host { get; set; }
		public string place_id { get; set; }
		public string name { get; set; } = string.Empty;
		public string nickname { get; set; } = string.Empty;
		public string region0 { get; set; } = "1";
		public string region_name0 { get; set; } = "W";
		public string region_name1 { get; set; } = "X";
		public string region_name2 { get; set; } = "Y";
		public string region_name3 { get; set; } = "Z";
		public string country { get; set; } = "JPN";
	}
}
