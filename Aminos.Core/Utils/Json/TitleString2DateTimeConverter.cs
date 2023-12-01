using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aminos.Core.Utils.Json
{
	public class TitleString2DateTimeConverter : JsonConverter<DateTime>
	{
		public const string Format = "yyyy-MM-dd HH:mm:ss.f";

		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var dateString = reader.GetString();
				if (DateTime.TryParseExact(dateString, Format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
					return result;
			}

			return JsonSerializer.Deserialize<DateTime>(ref reader, options);
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString(Format));
		}
	}
}
