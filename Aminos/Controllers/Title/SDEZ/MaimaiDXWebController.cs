using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.Title.SDEZ;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.Title.SDEZ
{
	[ApiController]
	[Route("api/SDEZ")]
	[Authorize(AuthRolePolicyString.UserRole)]
	public class MaimaiDXWebController : CommonWebAPIControllerBase
	{
		public MaimaiDXWebController(AminosDB aminosDB) : base(aminosDB)
		{

		}

		[HttpGet("GetUserPortraitPng")]
		public async ValueTask<IActionResult> GetUserPortraitPng(ulong targetUserId, MaimaiDXUserPortraitHandler handler)
		{
			var imageData = await handler.GetUserPortraitImageData(targetUserId);
			if (imageData is null)
				return StatusCode(404);
			return new FileContentResult(imageData, "image/jpeg");
		}
	}
}
