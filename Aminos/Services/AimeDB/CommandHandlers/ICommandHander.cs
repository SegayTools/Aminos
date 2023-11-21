using Aminos.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;

namespace Aminos.Services.AimeDB.CommandHandlers
{
	public interface ICommandHander
	{
		int HandleCommandId { get; }
		ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token);
	}
}
