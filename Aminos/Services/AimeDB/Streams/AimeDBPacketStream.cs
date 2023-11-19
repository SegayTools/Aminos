using Aminos.Models.AimeDB;
using Aminos.Utils.MethodExtensions;
using System;
using System.Buffers;
using System.IO;

namespace Aminos.Services.AimeDB.Streams
{
	public class AimeDBPacketStreamReaderWriter
	{
		private readonly Stream baseStream;

		public AimeDBPacketStreamReaderWriter(Stream baseStream)
		{
			this.baseStream = baseStream;
		}

		public async ValueTask<AimeDBPacket> ReadPacket(CancellationToken cancellationToken)
		{
			var headerSize = 16;
			var packet = new AimeDBPacket(16);
			using (var headerBufferDisp = ArrayPool<byte>.Shared.RentWithDisposable(headerSize))
			{
				var headerBuffer = headerBufferDisp.Memory.Slice(0, headerSize);
				await baseStream.ReadExactlyAsync(headerBuffer, cancellationToken);
				Encryption.Decrypt(headerBuffer.Span, packet.Buffer.Span);
			}

			packet.Resize(packet.TotalPacketLength, true);

			var contentSize = packet.TotalPacketLength - 16;
			using (var contentBufferDisp = ArrayPool<byte>.Shared.RentWithDisposable(contentSize))
			{
				var contentBuffer = contentBufferDisp.Memory.Slice(0, contentSize);
				await baseStream.ReadExactlyAsync(contentBuffer, cancellationToken);
				Encryption.Decrypt(contentBuffer.Span, packet.Buffer.Span[16..]);
			}

			return packet;
		}

		public async ValueTask WritePacket(AimeDBPacket packet, CancellationToken cancellationToken)
		{
			using var sendPacket = new AimeDBPacket(packet.TotalPacketLength);

			//make sure packet valid.
			packet.Buffer.WriteValue(0x0000, new byte[] { 0xa1, 0x3e, 0x30, 0x87 });
			packet.Buffer.WriteValue(0x0006, (short)packet.Buffer.Length);

			Encryption.Encrypt(packet.Buffer.Span, sendPacket.Buffer.Span);

			await baseStream.WriteAsync(sendPacket.Buffer, cancellationToken);
		}
	}
}
