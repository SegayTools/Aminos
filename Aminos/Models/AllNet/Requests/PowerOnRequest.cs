using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Requests
{
	public class PowerOnRequest : QueryPathSerializeBase
	{
		public string game_id { set; get; }
		public string ver { set; get; }
		public string serial { set; get; }
		public string ip { set; get; }
		public string firm_ver { set; get; }
		public string boot_ver { set; get; }
		public string encode { set; get; }
		public string format_ver { set; get; }
		public string hops { set; get; }
		public string token { set; get; }
	}
}
