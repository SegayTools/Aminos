using Aminos.Utils.MethodExtensions;
using System.IO.Compression;

namespace Aminos.Utils
{
	public static class Compression
	{
		public static async ValueTask<byte[]> DecompressZlib(byte[] src)
		{
			using var deflatStream = new ZLibStream(new MemoryStream(src), CompressionMode.Decompress);
			var decompBuffer = await deflatStream.ToByteArrayAsync();

			return decompBuffer;
		}

		public static async ValueTask<byte[]> DecompressDeflate(byte[] src)
		{
			using var deflatStream = new DeflateStream(new MemoryStream(src), CompressionMode.Decompress);
			var decompBuffer = await deflatStream.ToByteArrayAsync();

			return decompBuffer;
		}
	}
}
