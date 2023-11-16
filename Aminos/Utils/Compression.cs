using Aminos.Utils.MethodExtensions;
using System.IO.Compression;

namespace Aminos.Utils
{
	public static class Compression
	{
		public static async ValueTask<byte[]> Decompress(byte[] src)
		{
			using var deflatStream = new DeflateStream(new MemoryStream(src), CompressionMode.Decompress);
			var decompBuffer = await deflatStream.ToByteArray();

			return decompBuffer;
		}
	}
}
