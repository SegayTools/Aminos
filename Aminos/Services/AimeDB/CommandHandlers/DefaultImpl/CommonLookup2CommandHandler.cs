using Aminos.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class CommonLookup2CommandHandler : ICommandHander
	{
		private readonly ILogger<CommonLookup2CommandHandler> logger;
		private readonly AminosDB aminosDB;

		public int HandleCommandId => 0x000f;

		public CommonLookup2CommandHandler(ILogger<CommonLookup2CommandHandler> logger, AminosDB aminosDB)
		{
			this.logger = logger;
			this.aminosDB = aminosDB;
		}

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			var luid = reqPacket.Buffer[0x0020..0x002a];
			var luidStr = Convert.ToHexString(luid.Span);

			var card = await aminosDB.Cards.FirstOrDefaultAsync(x => luidStr == x.Luid);
			var aimeId = card?.AimeId ?? -1;

			using var respPacket = new AimeDBPacket(0x0130);
			respPacket.CommandID = 0x0010;
			respPacket.Result = 1;

			respPacket.Buffer.WriteValue(0x0020, aimeId);
			respPacket.Buffer.WriteValue(0x0024, (byte)0);

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}
