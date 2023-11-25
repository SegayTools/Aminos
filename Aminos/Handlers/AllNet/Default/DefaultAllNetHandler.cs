using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Models.AllNet.Requests;
using Aminos.Models.AllNet.Responses;
using Aminos.Models.General.Tables;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Utils;

namespace Aminos.Handlers.AllNet.Default
{
	[RegisterInjectable(typeof(IAllNetHandler))]
	internal class DefaultAllNetHandler : IAllNetHandler
	{
		private readonly AminosDB aminosDB;
		private readonly IKeychipSafeHandleAuthorization safeHandleAuthorization;

		public DefaultAllNetHandler(AminosDB aminosDB, IKeychipSafeHandleAuthorization safeHandleAuthorization)
		{
			this.aminosDB = aminosDB;
			this.safeHandleAuthorization = safeHandleAuthorization;
		}

		public async ValueTask<PowerOnResponseBase> HandlePowerOn(PowerOnRequest request, ConnectionInfo connectionInfo)
		{
			var keychip = await aminosDB.Keychips.FindAsync(request.serial);

			if (!await CheckKeychipIfValid(keychip))
				return default;

			var response = request.format_ver.StartsWith("2") ? HandlePowerOnAsV2(request) : HandlePowerOnAsV3(request);

			//process base common
			var localHost = connectionInfo.LocalIpAddress.MapToIPv4().ToString();

			response.host = localHost;
			response.uri = await GenerateGameUrl(request, connectionInfo, keychip);
			response.place_id = GetPlaceName(request);

			return response;
		}

		private string GetPlaceName(PowerOnRequest request)
		{
			//todo
			return "123";
		}

		private async ValueTask<string> GenerateGameUrl(PowerOnRequest request, ConnectionInfo connectionInfo, Keychip keychip)
		{
			var gameId = request.game_id;
			var host = connectionInfo.LocalIpAddress.MapToIPv4().ToString();
			var port = connectionInfo.LocalPort;
			var ver = request.ver;

			var safeHandle = await safeHandleAuthorization.GenerateSafeHandle(keychip);

			var url = $"http://{host}:{port}/{safeHandle}/";
			if (!string.IsNullOrWhiteSpace(gameId))
				url += $"{gameId}/{ver}/";

			return url;
		}

		private PowerOnResponseBase HandlePowerOnAsV2(PowerOnRequest request)
		{
			var now = DateTime.Now;
			return new PowerOnResponseV2()
			{
				year = now.Year,
				month = now.Month,
				day = now.Day,
				hour = now.Hour,
				minute = now.Minute,
				second = now.Second,
			};
		}

		private PowerOnResponseBase HandlePowerOnAsV3(PowerOnRequest request)
		{
			return new PowerOnResponseV3()
			{
				token = request.token,
				allnet_id = "456",
				client_timezone = "+0900",
				setting = string.Empty,
				res_ver = "3",
				utc_time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss").Substring(0, 19) + "Z"
			};
		}

		private async ValueTask<bool> CheckKeychipIfValid(Keychip keychip)
		{
			if (keychip is null)
				return false;

			return keychip.Enable;
		}
	}
}
