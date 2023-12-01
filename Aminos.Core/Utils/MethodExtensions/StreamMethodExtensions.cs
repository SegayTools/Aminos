using System.Text;

namespace Aminos.Core.Utils.MethodExtensions
{
	public static class StreamMethodExtensions
	{
		public static async ValueTask<byte[]> ToByteArrayAsync(this Stream stream)
		{
			using var ms = new MemoryStream();
			await stream.CopyToAsync(ms);
			return ms.ToArray();
		}

		public static byte[] ToByteArray(this Stream stream)
		{
			using var ms = new MemoryStream();
			stream.CopyTo(ms);
			return ms.ToArray();
		}

		public static ValueTask<string> DumpToString(this Stream stream)
			=> DumpToString(stream, Encoding.UTF8);

		public static async ValueTask<string> DumpToString(this Stream stream, Encoding encoder)
		{
			using var ms = new MemoryStream();
			await stream.CopyToAsync(ms);
			return encoder.GetString(ms.ToArray());
		}
	}
}
