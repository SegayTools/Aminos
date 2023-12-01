using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Aminos.Controllers.General
{
    [Route("api/Keychip")]
	[ApiController]
	[Authorize(AuthRolePolicyString.AdminRole)]
	public class KeychipController : CommonWebAPIControllerBase
	{
		private readonly GeneralKeychipHandler handler;

		public KeychipController(AminosDB aminosDB, GeneralKeychipHandler handler) : base(aminosDB)
		{
			this.handler = handler;
		}

		[HttpPost("Create")]
		[EnableRateLimiting("AntiBruteForce")]
		public async ValueTask<IActionResult> Create([FromForm] string keychipId)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.Create(user, keychipId);
			return Json(result);
		}

		[HttpPost("Delete")]
		public async ValueTask<IActionResult> Delete([FromForm] string keychipId)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.Delete(user, keychipId);
			return Json(result);
		}

		[HttpPost("Update")]
		public async ValueTask<IActionResult> Update([FromForm] string keychipId, [FromForm] string newName, [FromForm] bool newEnable)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.Update(user, keychipId, newName, newEnable);
			return Json(result);
		}

		[HttpGet("List")]
		public async ValueTask<IActionResult> List()
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.List(user);
			return Json(result);
		}
	}
}
