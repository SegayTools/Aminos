using Aminos.Core.Utils.MethodExtensions;
using System.Buffers;
using System.Text;
using static Aminos.Core.Utils.MethodExtensions.ArrayPoolMethodExtensions;

namespace Aminos.Core.Models.AimeDB
{
	public class AimeDBPacket : IDisposable
	{
		private IBufferWithDisposable<byte> bufferDisp;
		private ushort size;

		public Memory<byte> Buffer => bufferDisp is not null ? bufferDisp.Memory.Slice(0, size) : throw new ObjectDisposedException("AimeDBPackets had been disposed by GC or Dispose() called.");

		public bool IsValid => BitConverter.ToUInt16(Buffer.Span.Slice(0)) == 0xA13E;

		public ushort Version => BitConverter.ToUInt16(Buffer.Span.Slice(2));

		public ushort CommandID
		{
			get => BitConverter.ToUInt16(Buffer.Span.Slice(4));
			set => BitConverter.TryWriteBytes(Buffer.Span.Slice(4), value);
		}

		public ushort TotalPacketLength => BitConverter.ToUInt16(Buffer.Span.Slice(6));

		public ushort Result
		{
			get => BitConverter.ToUInt16(Buffer.Span.Slice(8));
			set => BitConverter.TryWriteBytes(Buffer.Span.Slice(8), value);
		}

		public string GameID => Encoding.ASCII.GetString(Buffer.Span[10..15]);

		public long StoreID => BitConverter.ToUInt32(Buffer.Span.Slice(16));

		public string KeychipID => Encoding.ASCII.GetString(Buffer.Span[20..31]);

		public AimeDBPacket(ushort size)
		{
			Resize(size);
		}

		public void Resize(ushort newSize, bool copyOldDataToNew = false)
		{
			if (newSize % 16 > 0)
				throw new ArgumentException($"packet size must be a multiple of 16");
			var newBufferDisp = ArrayPool<byte>.Shared.RentWithDisposable(newSize);
			newBufferDisp.Memory.ClearValues();

			if (copyOldDataToNew && bufferDisp is not null)
			{
				if (size > newSize)
					throw new ArgumentException($"Can't copy old data to new because size not enough.");
				bufferDisp.Memory.CopyTo(newBufferDisp.Memory);
			}

			bufferDisp?.Dispose();
			bufferDisp = newBufferDisp;
			size = newSize;

			Buffer.WriteValue(0x0000, new byte[] { 0xa1, 0x3e, 0x30, 0x87 });
			Buffer.WriteValue(0x0006, size);
		}

		public void Dispose()
		{
			bufferDisp?.Dispose();
			bufferDisp = null;
		}

		public override string ToString() => $"CommandID: {CommandID}, GameID: {GameID}, KeychipID: {KeychipID}, TotalPacketLength: {TotalPacketLength}, StoreID: {StoreID}, Result: {Result}  ";
	}
}
