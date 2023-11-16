using Aminos.Handlers.AllNet;
using Aminos.Models.AllNet.Reesponses;
using Aminos.Models.AllNet.Requests;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Aminos.Controllers.AllNet
{
	[ApiController]
	[Route("sys")]
	public class AllNetController : ControllerBase
	{
		private readonly ILogger<AllNetController> logger;
		private readonly IAllNetHandler allNetHandler;

		public AllNetController(ILogger<AllNetController> logger, IAllNetHandler allNetHandler)
		{
			this.logger = logger;
			this.allNetHandler = allNetHandler;
		}

		[HttpGet("test")]
		[HttpGet("/")]
		public IActionResult Test() => Content("Server running");

		[HttpGet("naomitest.html")]
		public IActionResult NaomiTest() => Content("naomi ok");

		[HttpPost("servlet/PowerOn")]
		public async ValueTask<IActionResult> PowerOn()
		{
			var body = Convert.FromBase64String(await HttpContext.Request.Body.DumpToString());
			var decompBuffer = await Compression.Decompress(body);

			var queryString = Encoding.UTF8.GetString(decompBuffer).Trim();
			var request = PowerOnRequest.ParseQueryPath(queryString);

			if ((await allNetHandler.HandlePowerOn(request, HttpContext.Connection)) is PowerOnResponseBase response)
				return Content(response.GenerateQueryPath(), "text/plain");

			return StatusCode(403);
		}
	}
}
