using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class CampaignCommandHandler : ICommandHander
	{
		public int HandleCommandId => 0x000b;

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			using var respPacket = new AimeDBPacket(0x0200);

			respPacket.CommandID = 0x000c;
			respPacket.Result = 1;

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}
