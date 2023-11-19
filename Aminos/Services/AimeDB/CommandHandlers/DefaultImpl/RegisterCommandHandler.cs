using Aminos.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Models.AimeDB.Requests;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class RegisterCommandHandler : ICommandHander
	{
		private readonly AminosDB aminosDB;

		public int HandleCommandId => 0x0005;

		public RegisterCommandHandler(AminosDB aminosDB)
		{
			this.aminosDB = aminosDB;
		}

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			var luid = reqPacket.AquaData[0x0020..0x002a];
			var luidStr = Convert.ToHexString(luid.Span);

			var status = 0;
			var aimeId = 0L;

			var card = await aminosDB.Cards.FirstOrDefaultAsync(x => luidStr.Equals(x.Luid, StringComparison.InvariantCultureIgnoreCase));
			if (card is null)
			{
				status = 1;
				aimeId = card.AimeId;
			}

			using var respPacket = new AimeDBPacket(0x0030);
			respPacket.CommandID = 0x0006;
			respPacket.Result = (ushort)status;
			respPacket.Buffer.WriteValue(0x0020, aimeId);

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}
