using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils.MethodExtensions;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class TouchCommandHandler : ICommandHander
	{
		public int HandleCommandId => 0x000d;

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			using var respPacket = new AimeDBPacket(0x0050);
			respPacket.CommandID = 0x000e;
			respPacket.Result = 1;
			respPacket.Buffer.WriteValue<short>(0x0020, 0x006f);
			respPacket.Buffer.WriteValue<short>(0x0024, 0x0001);

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}
