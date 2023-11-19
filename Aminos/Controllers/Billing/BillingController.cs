using Aminos.Models.Billing.Requests;
using Aminos.Models.Billing.Responses;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Aminos.Controllers.Billing
{
	[ApiController]
	public class BillingController : ControllerBase
	{
		private readonly ILogger<BillingController> logger;
		private byte[] fileBytes;
		private RSA rsa;
		private SHA1 sha1;
		private object locker = new();

		public BillingController(ILogger<BillingController> logger)
		{
			this.logger = logger;
		}

		[HttpPost("/request")]
		[HttpPost("/request.php")]
		public async ValueTask<IActionResult> OnBilling()
		{
			var bytes = await HttpContext.Request.Body.ToByteArrayAsync();
			var decompBytes = await Compression.DecompressDeflate(bytes);
			var billingContent = Encoding.UTF8.GetString(decompBytes).Trim();

			var billingLines = billingContent.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
			for (var i = 0; i < billingLines.Length; i++)
				logger.LogDebug($"billingLines[{i}]: {billingLines[i]}");

			var request = new BillingRequest();
			request.ParseQueryPath(billingLines[0]);

			InitRsa();

			var keychipId = request.keychipid;

			var response = new BillingResponse()
			{
				result = 0,
				waittime = 100,
				linelimit = 1,
				message = "",
				playlimit = 1024,
				protocolver = "1.000",
				nearfull = 66048,
				fixlogcnt = 0,
				fixinterval = 5,
				playhistory = "000000/0:000000/0:000000/0"
			};

			response.playlimitsig = SignWithKey(keychipId, 1024);
			response.nearfullsig = SignWithKey(keychipId, 66048);

			return Content(response.GenerateQueryPath() + "\n", "text/plain");
		}

		private void InitRsa()
		{
			lock (locker)
			{
				if (fileBytes == null)
				{
					using var fileStream = typeof(Program).Assembly.GetManifestResourceStream("Aminos.Resources.Billing.billing.private.txt");
					var bytes = fileStream.ToByteArray();

					var str = Encoding.UTF8.GetString(bytes);

					rsa = new RSACryptoServiceProvider();
					sha1 = SHA1.Create();
					rsa.ImportFromPem(str);
					fileBytes = bytes;
				};
			}
		}

		private string SignWithKey(string keychipId, int val)
		{
			//1int(4bytes) + 1string(11bytes)
			var buffer = new byte[15];
			buffer.AsSpan().WriteValue(0, val);
			buffer.AsSpan().WriteValue(4, Encoding.ASCII.GetBytes(keychipId.Replace("-", string.Empty)));

			var sha1Result = sha1.ComputeHash(buffer);
			var rsaResult = rsa.SignHash(sha1Result, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

			return Convert.ToHexString(rsaResult);
		}

		private Dictionary<string, string> SplitQueryString(string queryString)
		{
			var map = new Dictionary<string, string>();
			foreach (var kvp in queryString.Split("&"))
			{
				var s = kvp.Split('=');
				var key = s.ElementAtOrDefault(0);
				var value = s.ElementAtOrDefault(1);

				map[key] = value;
			}
			return map;
		}
	}
}
