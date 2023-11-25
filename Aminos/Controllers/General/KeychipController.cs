using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.General
{
	[Route("api/Keychip")]
	[ApiController]
	[Authorize(Roles = AuthRolePolicyString.AdminRole)]
	public class KeychipController : GeneralAPIControllerBase
	{
		public KeychipController(AminosDB aminosDB) : base(aminosDB)
		{

		}

		[HttpPost("Create")]
		public async ValueTask<IActionResult> Create(string keychipId, GeneralKeychipHandler handler)
		{
			var user = await GetUser();

			var result = await handler.Create(user, keychipId);
			return Json(result);
		}

		[HttpPost("Delete")]
		public async ValueTask<IActionResult> Delete(string keychipId, GeneralKeychipHandler handler)
		{
			var user = await GetUser();

			var result = await handler.Delete(user, keychipId);
			return Json(result);
		}
	}
}
