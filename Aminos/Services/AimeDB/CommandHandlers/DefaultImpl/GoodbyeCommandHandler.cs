using Aminos.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Models.AimeDB.Requests;
using Aminos.Services.AimeDB.Streams;
using System.Buffers;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class GoodbyeCommandHandler : ICommandHander
	{
		public int HandleCommandId => 0x0066;

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			return true;
		}
	}
}
