using System.Text;

namespace Aminos.Utils.MethodExtensions
{
	public static class BinaryReaderMethodExtensions
	{
		public static string ReadString(this BinaryReader reader, int length)
			=> reader.ReadString(length, Encoding.UTF8);

		public static string ReadString(this BinaryReader reader, int length, Encoding encode)
			=> encode.GetString(reader.ReadBytes(length));
	}
}
