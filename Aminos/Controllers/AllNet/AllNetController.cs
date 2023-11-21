﻿using Aminos.Handlers.AllNet;
using Aminos.Models.AllNet.Requests;
using Aminos.Models.AllNet.Responses;
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
		private readonly IAllNetHandler allNetHandler;

		public AllNetController(IAllNetHandler allNetHandler)
		{
			this.allNetHandler = allNetHandler;
		}

		[HttpGet("test")]
		[HttpGet("/")]
		public IActionResult Test() => Content("Server running");

		[HttpGet("naomitest.html")]
		public IActionResult NaomiTest() => Content("naomi ok");

		[HttpPost("servlet/DownloadOrder")]
		public async ValueTask<IActionResult> DownloadOrder()
		{
			//todo
			var content = await Decode(await HttpContext.Request.Body.DumpToString());
			return Content("stat=1&serial=A69E01A8888\n", "text/plain");
		}

		[HttpPost("servlet/PowerOn")]
		public async ValueTask<IActionResult> PowerOn()
		{
			var queryString = await Decode(await HttpContext.Request.Body.DumpToString());
			var request = new PowerOnRequest();
			request.ParseQueryPath(queryString);

			if ((await allNetHandler.HandlePowerOn(request, HttpContext.Connection)) is PowerOnResponseBase response)
				return Content(response.GenerateQueryPath() + "\n", "text/plain");

			return StatusCode(403);
		}

		private async ValueTask<string> Decode(string requestBody)
		{
			var body = Convert.FromBase64String(requestBody);
			var decompBuffer = await Compression.DecompressZlib(body);

			var queryString = Encoding.UTF8.GetString(decompBuffer).Trim();
			return queryString;
		}
	}
}
