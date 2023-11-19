﻿using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.AimeDB;
using Aminos.Models.AimeDB.Requests;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils.MethodExtensions;
using System.Buffers;

namespace Aminos.Services.AimeDB.CommandHandlers.DefaultImpl
{
	[RegisterInjectable(typeof(ICommandHander))]
	public class Unknown19CommandHandler : ICommandHander
	{
		public int HandleCommandId => 0x0013;

		public async ValueTask<bool> Handle(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			using var respPacket = new AimeDBPacket(0x0040);

			respPacket.CommandID = 0x0014;
			respPacket.Result = 1;

			await stream.WritePacket(respPacket, token);
			return true;
		}
	}
}