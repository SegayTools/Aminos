using System.Text.Json;

namespace Aminos.Utils
{
	public static class JsonSerializeOptions
	{
		public static JsonSerializerOptions NonIntendSerializeOption { get; } = new JsonSerializerOptions()
		{
			WriteIndented = false
		};
	}
}
