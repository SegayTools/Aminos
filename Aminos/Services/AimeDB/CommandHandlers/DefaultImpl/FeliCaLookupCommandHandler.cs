using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils.MethodExtensions;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class FeliCaLookupCommandHandler : ICommandHander
	{
		private readonly ILogger<FeliCaLookupCommandHandler> logger;

		public int HandleCommandId => 0x0001;

		public FeliCaLookupCommandHandler(ILogger<FeliCaLookupCommandHandler> logger)
		{
			this.logger = logger;
		}

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			var idm = reqPacket.Buffer[0x0020..0x0028];
			//var pmm = content[0x0028..0x0030];

			var accessCode = Convert.ToHexString(idm.Span);
			accessCode = accessCode.PadRight(20, '0');
			var accessCodeBytes = Convert.FromHexString(accessCode);

			using var respPacket = new AimeDBPacket(0x0030);
			respPacket.CommandID = 0x0003;
			respPacket.Result = 1;
			respPacket.Buffer.WriteValue(0x0024, accessCodeBytes);

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}
