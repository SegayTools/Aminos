using Aminos.Core.Models.AimeDB;
using Aminos.Services.AimeDB.CommandHandlers;
using Aminos.Services.AimeDB.Streams;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace Aminos.Services.AimeDB
{
	public class AimeDBService : BackgroundService
	{
		private readonly ILoggerFactory loggerFactory;
		private readonly IServiceScope scope;
		private readonly IEnumerable<ICommandHander> commandHandlers;

		private const int PORT = 22345;
		private int clientGenId = 0;

		public AimeDBService(ILoggerFactory loggerFactory, IServiceProvider provider)
		{
			this.loggerFactory = loggerFactory;

			scope = provider.CreateScope();
			commandHandlers = scope.ServiceProvider.GetServices<ICommandHander>();
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var tcpListener = new TcpListener(IPAddress.Any, PORT);

			if (stoppingToken.IsCancellationRequested)
				return;
			tcpListener.Start();
			var logger = loggerFactory.CreateLogger<AimeDBService>();
			logger.LogInformation($"AimeDB server started on port {PORT}.");
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					var client = await tcpListener.AcceptTcpClientAsync(stoppingToken);
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
			var logger = loggerFactory.CreateLogger($"AimeDBClient-{clientGenId++}");
			using var stream = client.GetStream();
			var packetStream = new AimeDBPacketStreamReaderWriter(stream, logger);

			var headerBuffer = new byte[16];
			while (client.Connected && !stoppingToken.IsCancellationRequested)
			{
				if (!stream.DataAvailable)
					continue;

				using var packet = await packetStream.ReadPacket(stoppingToken);
				using var loggerScope = logger.BeginScope("Recv packet:" + packet.ToString());

				await ProcessPacket(logger, packetStream, packet, stoppingToken);
			}
			client.Close();
			logger.LogDebug($"ProcessClient() done");
		}

		private async Task ProcessPacket(ILogger logger, AimeDBPacketStreamReaderWriter stream, AimeDBPacket reqPacket, CancellationToken token)
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
