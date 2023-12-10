using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.General;

[Route("api/Card")]
[ApiController]
[Authorize(AuthRolePolicyString.UserRole)]
public class CardController : CommonWebAPIControllerBase
{
    private readonly CardHandler handler;

    public CardController(AminosDB aminosDB, CardHandler handler) : base(aminosDB)
    {
        this.handler = handler;
    }

    [HttpPost("BindCardToUser")]
    public async ValueTask<IActionResult> BindCardToUser([FromForm] string accessCode)
    {
        var user = await GetCurrentRequestUser();

        var result = await handler.BindCardToUser(user, accessCode);
        return Json(result);
    }
    
    [HttpPost("UnbindCardToUser")]
    public async ValueTask<IActionResult> UnbindCardToUser([FromForm] string accessCode)
    {
        var user = await GetCurrentRequestUser();

        var result = await handler.UnbindCardToUser(user, accessCode);
        return Json(result);
    }
    
    [HttpGet("GetCards")]
    public async ValueTask<IActionResult> GetCards([FromForm] string accessCode)
    {
        var user = await GetCurrentRequestUser();

        var result = await handler.GetCards(user);
        return Json(result);
    }
}