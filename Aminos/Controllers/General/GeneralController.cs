using Aminos.Authorization;
using Aminos.Core.Models.General.Tables;
using Aminos.Databases;
using Aminos.Handlers.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.General;

[Route("api/General")]
[ApiController]
public class GeneralController : CommonWebAPIControllerBase
{
    private readonly GeneralHandler handler;

    public GeneralController(AminosDB aminosDB, GeneralHandler handler) : base(aminosDB)
    {
        this.handler = handler;
    }

    [HttpGet("GetAnnouncements")]
    public async ValueTask<IActionResult> GetAnnouncements([FromQuery] int takeCount, [FromQuery] int skipCount)
    {
        var result = await handler.GetAnnouncements(takeCount, skipCount);
        return Json(result);
    }

    [HttpPost("AddAnnouncement")]
    [Authorize(AuthRolePolicyString.AdminRole)]
    public async ValueTask<IActionResult> AddAnnouncement([FromBody] Announcement announcement)
    {
        var user = await GetCurrentRequestUser();

        var result = await handler.AddAnnouncement(user, announcement);
        return Json(result);
    }
    
    [HttpPost("DeleteAnnouncement")]
    [Authorize(AuthRolePolicyString.AdminRole)]
    public async ValueTask<IActionResult> DeleteAnnouncement([FromBody] int announcementId)
    {
        var user = await GetCurrentRequestUser();
        var result = await handler.DeleteAnnouncement(user, announcementId);
        return Json(result);
    }

    [HttpGet("GetGeneralStatistic")]
    public async ValueTask<IActionResult> GetGeneralStatistic()
    {
        var result = await handler.GetGeneralStatistic();
        return Json(result);
    }
    
    [HttpGet("GetGameStatistic")]
    public async ValueTask<IActionResult> GetGameStatistic()
    {
        var result = await handler.GetGameStatistic();
        return Json(result);
    }
}