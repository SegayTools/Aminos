using Aminos.Models.AllNet.Reesponses;
using Aminos.Models.AllNet.Requests;
using Aminos.Utils;
using System.Net;

namespace Aminos.Handlers.AllNet.Default
{
	internal class DefaultAllNetHandler : IAllNetHandler
	{
		public async ValueTask<PowerOnResponseBase> HandlePowerOn(PowerOnRequest request, ConnectionInfo connectionInfo)
		{
			if (!await CheckKeychipIfValid(request.Serial))
				return default;

			var response = request.FormatVersion.StartsWith("2") ? HandlePowerOnAsV2(request) : HandlePowerOnAsV3(request);

			//process base common
			var localHost = connectionInfo.LocalIpAddress.ToString();

			response.Host = localHost;
			response.Uri = GenerateGameUrl(request, connectionInfo);
			response.PlaceId = GetPlaceName(request);

			return response;
		}

		private string GetPlaceName(PowerOnRequest request)
		{
			//todo
			return "Aminos' Heaven";
		}

		public string ConvertSerialToIdentityStr(string serial) => SimpleCryptography.Encrypt(serial).Replace("=", "_");
		public string ConvertIdentityStrToSerial(string identityStr) => SimpleCryptography.Decrypt(identityStr.Replace("_", "="));

		private string GenerateGameUrl(PowerOnRequest request, ConnectionInfo connectionInfo)
		{
			var gameId = request.GameId;
			var host = connectionInfo.LocalIpAddress?.ToString();
			var port = connectionInfo.LocalPort;
			var ver = request.Version;
			var identityStr = ConvertSerialToIdentityStr(request.Serial);

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
				Year = now.Year,
				Month = now.Month,
				Day = now.Day,
				Hour = now.Hour,
				Minute = now.Minute,
				Second = now.Second,
			};
		}

		private PowerOnResponseBase HandlePowerOnAsV3(PowerOnRequest request)
		{
			return new PowerOnResponseV3()
			{
				Token = request.Token,
				AllnetId = "456",
				ClientTimezone = "+0900",
				Setting = string.Empty,
				ResVersion = "3",
				UtcTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss").Substring(0, 19) + "Z"
			};
		}

		private async ValueTask<bool> CheckKeychipIfValid(string serial)
		{
			if (string.IsNullOrWhiteSpace(serial))
				return false;

			return true;
		}
	}
}
