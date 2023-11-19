using Aminos.Models.AimeDB;
using Aminos.Models.AimeDB.Requests;
using Aminos.Services.AimeDB.CommandHandlers;
using Aminos.Services.AimeDB.Streams;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Aminos.Services.AimeDB
{
	public class AimeDBService : BackgroundService
	{
		private readonly ILogger<AimeDBService> logger;
		private readonly IServiceScope scope;
		private readonly IEnumerable<ICommandHander> commandHandlers;

		private const int PORT = 22345;

		public AimeDBService(ILogger<AimeDBService> logger, IServiceProvider provider)
		{
			this.logger = logger;

			scope = provider.CreateScope();
			commandHandlers = scope.ServiceProvider.GetServices<ICommandHander>();
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var tcpListener = new TcpListener(IPAddress.Any, PORT);

			if (stoppingToken.IsCancellationRequested)
				return;
			tcpListener.Start();
			logger.LogInformation($"AimeDB server started on port {PORT}.");
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					var client = await tcpListener.AcceptTcpClientAsync(stoppingToken);
					using var loggerScope = logger.BeginScope($"Accept a client from IP({client.Client.RemoteEndPoint})");
					var _ = Task.Run(() =>
					{
						try
						{
							ProcessClient(client, stoppingToken);
						}
						catch (Exception e)
						{
							logger.LogError(e, $"client throw exception, stop processing.");
						}
					}
					, stoppingToken);
				}
				catch (Exception e)
				{
					logger.LogError(e, $"process AimeDB client throw exception:{e.Message}");
				}
			}
			tcpListener.Stop();
			logger.LogInformation($"AimeDB server stoped.");
		}

		private async void ProcessClient(TcpClient client, CancellationToken stoppingToken)
		{
			using var stream = client.GetStream();
			var packetStream = new AimeDBPacketStreamReaderWriter(stream);

			var headerBuffer = new byte[16];
			while (client.Connected && !stoppingToken.IsCancellationRequested)
			{
				if (!stream.DataAvailable)
					continue;

				using var packet = await packetStream.ReadPacket(stoppingToken);

				logger.LogDebug($"got packet header: {packet}");

				await ProcessPacket(packetStream, packet, stoppingToken);
			}
			client.Close();
			logger.LogDebug($"called Close()");
		}

		private async Task ProcessPacket(AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
		{
			if (commandHandlers.FirstOrDefault(x => x.HandleCommandId == reqPacket.CommandID) is not ICommandHander handler)
			{
				logger.LogWarning($"No command handler can process commandId:{reqPacket.CommandID}.");
				return;
			}

			logger.LogDebug($"ProcessPacket() handler = {handler.GetType().Name}");
			await handler.Handle(stream, reqPacket, token);
			logger.LogDebug($"ProcessPacket() done.");
		}

		public override void Dispose()
		{
			base.Dispose();
			scope.Dispose();
		}
	}
}
