using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Aminos.Controllers.General
{
    [Route("api/Account")]
	[ApiController]
	public class UserAccountController : CommonWebAPIControllerBase
	{
		private readonly GeneralUserAccountHandler handler;

		public UserAccountController(AminosDB aminosDB, GeneralUserAccountHandler handler) : base(aminosDB)
		{
			this.handler = handler;
		}

		[HttpPost("Login")]
		public async ValueTask<IActionResult> Login([FromForm] string userName, [FromForm] string passwordHash)
		{
			var result = await handler.Login(userName, passwordHash, HttpContext);
			return Json(result);
		}

		[HttpPost("Logout")]
		public async ValueTask<IActionResult> Logout()
		{
			var result = await handler.Logout(HttpContext);
			return Json(result);
		}

		[HttpPost("Register")]
		public async ValueTask<IActionResult> Register([FromForm] string userName, [FromForm] string passwordHash, [FromForm] string email)
		{
			var result = await handler.Register(userName, passwordHash, email);
			return Json(result);
		}

		[HttpPost("UpdatePassword")]
		public async ValueTask<IActionResult> UpdatePassword([FromForm] string newPasswordHash)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.UpdatePassword(user, newPasswordHash);
			return Json(result);
		}

		[HttpPost("Update")]
		public async ValueTask<IActionResult> Update([FromForm] string newPasswordHash, [FromForm] string newEmail, [FromForm] string newName)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.Update(user, newEmail, newName);
			return Json(result);
		}

		[HttpGet("Get")]
		public async ValueTask<IActionResult> Get()
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.Get(user);
			return Json(result);
		}

		[Authorize(AuthRolePolicyString.AdminRole)]
		[HttpPost("ChangeUserRole")]
		public async ValueTask<IActionResult> ChangeUserRole([FromForm] Guid targetUserGuid, [FromForm] AuthRolePolicy newRole)
		{
			var user = await GetCurrentRequestUser();

			var result = await handler.ChangeUserRole(user, targetUserGuid, newRole);
			return Json(result);
		}
	}
}
