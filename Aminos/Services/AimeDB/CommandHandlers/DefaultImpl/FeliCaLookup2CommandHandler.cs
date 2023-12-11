using Aminos.Core.Models.AimeDB;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Utils.MethodExtensions;
using Aminos.Databases;
using Aminos.Services.AimeDB.Streams;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl;

[RegisterInjectable(typeof(ICommandHander))]
public class FeliCaLookup2CommandHandler : ICommandHander
{
    private readonly AminosDB aminosDB;

    public FeliCaLookup2CommandHandler(AminosDB aminosDB)
    {
        this.aminosDB = aminosDB;
    }

    public int HandleCommandId => 0x0011;

    public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket,
        CancellationToken token)
    {
        var idm = reqPacket.Buffer[0x0030..0x0038];
        var pmm = reqPacket.Buffer[0x0038..0x0040];

        var accessCode = string.Concat(idm.ToArray().Select(x => x.ToString()));
        accessCode = accessCode.PadLeft(20, '0');
        var accessCodeBytes = Convert.FromHexString(accessCode);

        var card = await aminosDB.Cards.FirstOrDefaultAsync(x => accessCode == x.Luid);
        var aimeId = card?.AimeId ?? -1;

        using var respPacket = new AimeDBPacket(0x0140);

        respPacket.CommandID = 0x0012;
        respPacket.Result = 1;
        respPacket.Buffer.WriteValue(0x0020, aimeId);
        respPacket.Buffer.WriteValue(0x0024, 0xFFFFFFFF);
        respPacket.Buffer.WriteValue(0x0028, 0xFFFFFFFF);
        respPacket.Buffer.WriteValue(0x002c, accessCodeBytes);
        respPacket.Buffer.WriteValue<short>(0x0037, 0x0001);

        await stream.WritePacket(respPacket, token);
        return true;
    }
}