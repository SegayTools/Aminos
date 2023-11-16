using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Aminos.Models.AllNet.Reesponses
{
	public abstract class PowerOnResponseBase
	{
		[JsonPropertyName("stat")]
		public int Status { get; set; } = 1;
		[JsonPropertyName("uri")]
		public string Uri { get; set; }
		[JsonPropertyName("host")]
		public string Host { get; set; }
		[JsonPropertyName("place_id")]
		public string PlaceId { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
		[JsonPropertyName("nickname")]
		public string Nickname { get; set; } = string.Empty;
		[JsonPropertyName("region0")]
		public string Region0 { get; set; } = "1";
		[JsonPropertyName("region_name0")]
		public string RegionName0 { get; set; } = "W";
		[JsonPropertyName("region_name1")]
		public string RegionName1 { get; set; } = "X";
		[JsonPropertyName("region_name2")]
		public string RegionName2 { get; set; } = "Y";
		[JsonPropertyName("region_name3")]
		public string RegionName3 { get; set; } = "Z";
		[JsonPropertyName("country")]
		public string Country { get; set; } = "JPN";

		public string GenerateQueryPath()
		{
			StringBuilder queryBuilder = new StringBuilder();

			Type type = GetType();
			PropertyInfo[] properties = type.GetProperties();

			foreach (PropertyInfo property in properties)
			{
				string propertyName = (property.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
									  .FirstOrDefault() as JsonPropertyNameAttribute)?.Name;

				propertyName = propertyName ?? property.Name;

				object value = property.GetValue(this);

				queryBuilder.Append($"{propertyName}={value}&");
			}

			if (queryBuilder.Length > 0)
				queryBuilder.Length -= 1;

			return queryBuilder.ToString();
		}
	}
}
