using Aminos.Utils.MethodExtensions;
using System.IO;
using System.Text;

namespace Aminos.Models.AimeDB.Requests
{
	public struct CommonPacketHeader
	{
		public ushort Version { get; set; }
		public ushort CommandId { get; set; }
		public ushort Length { get; set; }

		public static CommonPacketHeader DeserializeBinary(byte[] stream)
		{
			var reader = new BinaryReader(new MemoryStream(stream));
			{
				CommonPacketHeader header = new CommonPacketHeader();
				reader.ReadUInt16();//magic~
				header.Version = reader.ReadUInt16();
				header.CommandId = reader.ReadUInt16();
				header.Length = reader.ReadUInt16();
				return header;
			}
		}
	}
}
