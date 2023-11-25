using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.General
{
    [Route("api/Account")]
    [ApiController]
    public class UserAccountController : GeneralAPIControllerBase
    {
		public UserAccountController(AminosDB aminosDB) : base(aminosDB)
		{

		}

		[HttpPost("Login")]
        public async ValueTask<IActionResult> Login(string userName, string passwordHash, GeneralUserAccountHandler handler)
        {
            var result = await handler.Login(userName, passwordHash, HttpContext);
            return Json(result);
        }

        [HttpPost("Logout")]
        public async ValueTask<IActionResult> Logout(GeneralUserAccountHandler handler)
        {
            var result = await handler.Logout(HttpContext);
            return Json(result);
        }

        [HttpPost("Register")]
        public async ValueTask<IActionResult> Register(string userName, string passwordHash, string email, GeneralUserAccountHandler handler)
        {
            var result = await handler.Register(userName, passwordHash, email);
            return Json(result);
        }
    }
}
