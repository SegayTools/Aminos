using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Requests
{
	public class PowerOnRequest
	{
		[JsonPropertyName("game_id")]
		public string GameId { set; get; }

		[JsonPropertyName("ver")]
		public string Version { set; get; }

		[JsonPropertyName("serial")]
		public string Serial { set; get; }

		[JsonPropertyName("ip")]
		public string IPAddress { set; get; }

		[JsonPropertyName("firm_ver")]
		public string FirmVersion { set; get; }

		[JsonPropertyName("boot_ver")]
		public string BootVersion { set; get; }

		[JsonPropertyName("encode")]
		public string Encode { set; get; }

		[JsonPropertyName("format_ver")]
		public string FormatVersion { set; get; }

		[JsonPropertyName("hops")]
		public string Hops { set; get; }

		[JsonPropertyName("token")]
		public string Token { set; get; }

		private static readonly IDictionary<string, Action<string, PowerOnRequest>> cachedSetter = typeof(PowerOnRequest).GetProperties()
			.ToDictionary(
			x => x.GetCustomAttribute<JsonPropertyNameAttribute>().Name ?? x.Name,
			x => new Action<string, PowerOnRequest>((val, obj) => x.SetValue(obj, val))).ToImmutableDictionary();

		public static PowerOnRequest ParseQueryPath(string queryString)
		{
			var req = new PowerOnRequest();
			foreach (var pair in queryString.Split("&"))
			{
				var split = pair.Split("=", StringSplitOptions.RemoveEmptyEntries);
				var name = split.ElementAtOrDefault(0);
				var value = split.ElementAtOrDefault(1);

				if (cachedSetter.TryGetValue(name, out var setter))
					setter(value, req);
			}
			return req;
		}
	}
}
