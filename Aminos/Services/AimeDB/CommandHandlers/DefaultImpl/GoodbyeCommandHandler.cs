using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class GoodbyeCommandHandler : ICommandHander
	{
		public int HandleCommandId => 0x0066;

		public ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			return ValueTask.FromResult(true);
		}
	}
}
