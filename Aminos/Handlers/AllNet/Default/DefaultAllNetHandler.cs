using Aminos.Models.AllNet.Requests;
using Aminos.Models.AllNet.Responses;
using Aminos.Utils;

namespace Aminos.Handlers.AllNet.Default
{
	internal class DefaultAllNetHandler : IAllNetHandler
	{
		public async ValueTask<PowerOnResponseBase> HandlePowerOn(PowerOnRequest request, ConnectionInfo connectionInfo)
		{
			if (!await CheckKeychipIfValid(request.serial))
				return default;

			var response = request.format_ver.StartsWith("2") ? HandlePowerOnAsV2(request) : HandlePowerOnAsV3(request);

			//process base common
			var localHost = connectionInfo.LocalIpAddress.MapToIPv4().ToString();

			response.host = localHost;
			response.uri = GenerateGameUrl(request, connectionInfo);
			response.place_id = GetPlaceName(request);

			return response;
		}

		private string GetPlaceName(PowerOnRequest request)
		{
			//todo
			return "123";
		}

		public string ConvertSerialToIdentityStr(string serial) => SimpleCryptography.Encrypt(serial).Replace("=", "_");
		public string ConvertIdentityStrToSerial(string identityStr) => SimpleCryptography.Decrypt(identityStr.Replace("_", "="));

		private string GenerateGameUrl(PowerOnRequest request, ConnectionInfo connectionInfo)
		{
			var gameId = request.game_id;
			var host = connectionInfo.LocalIpAddress.MapToIPv4().ToString();
			var port = connectionInfo.LocalPort;
			var ver = request.ver;
			var identityStr = ConvertSerialToIdentityStr(request.serial);

			var url = $"http://{host}:{port}/{identityStr}/";
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

		private ValueTask<bool> CheckKeychipIfValid(string serial)
		{
			if (string.IsNullOrWhiteSpace(serial))
				return ValueTask.FromResult(false);

			return ValueTask.FromResult(true);
		}
	}
}
