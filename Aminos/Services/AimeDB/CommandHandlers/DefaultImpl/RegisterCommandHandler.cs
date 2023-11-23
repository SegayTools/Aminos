using Aminos.Databases;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Models.General.Tables;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class RegisterCommandHandler : ICommandHander
	{
		private readonly AminosDB aminosDB;
		private readonly ILogger<RegisterCommandHandler> logger;

		public int HandleCommandId => 0x0005;

		public RegisterCommandHandler(AminosDB aminosDB, ILogger<RegisterCommandHandler> logger)
		{
			this.aminosDB = aminosDB;
			this.logger = logger;
		}

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			var luid = reqPacket.Buffer[0x0020..0x002a];
			var luidStr = Convert.ToHexString(luid.Span);

			var status = 0;
			var aimeId = 0L;

			var card = await aminosDB.Cards.FirstOrDefaultAsync(x => luidStr == x.Luid);
			if (card is null)
			{
				var rand = new Random();
				var extId = rand.Next(99999999).ToString();
				while (await aminosDB.Cards.AnyAsync(x => x.ExtId == extId))
					extId = rand.Next(99999999).ToString();

				card = new Card()
				{
					Luid = luidStr,
					RegisterTime = DateTime.Now,
					AccessTime = DateTime.Now,
					ExtId = extId,
				};

				await aminosDB.Cards.AddAsync(card);

				status = 1;
				aimeId = card.AimeId;

				await aminosDB.SaveChangesAsync();
				logger.LogInformation($"Register new card: {JsonSerializer.Serialize(card, JsonSerializeOptions.NonIntendSerializeOption)}");
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
