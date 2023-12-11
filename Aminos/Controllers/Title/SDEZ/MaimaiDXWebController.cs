using Aminos.Authorization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Databases;
using Aminos.Handlers.Title.SDEZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aminos.Controllers.Title.SDEZ;

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

    [HttpGet("GetUserDetail")]
    public async ValueTask<IActionResult> GetUserDetail(UserDetailRequestVO request, MaimaiDXUserDataHandler handler)
    {
        var response = await handler.GetUserData(request);
        return Json(response);
    }

    [HttpGet("GetUserOption")]
    public async ValueTask<IActionResult> GetUserOption(UserOptionRequestVO request, MaimaiDXUserOptionHandler handler)
    {
        var response = await handler.GetUserOption(request);
        return Json(response);
    }

    [HttpGet("GetUserExtend")]
    public async ValueTask<IActionResult> GetUserExtend(UserExtendRequestVO request, MaimaiDXUserExtendHandler handler)
    {
        var response = await handler.GetUserExtend(request);
        return Json(response);
    }

    [HttpGet("GetUserCalculatedRatingList")]
    public async ValueTask<IActionResult> GetUserCalculatedRatingList(ulong userId,
        MaimaiDXUserRatingHandler handler)
    {
        var response = await handler.GetUserCalculatedRatingList(userId);
        return Json(response);
    }

    [HttpPost("AddMusicData")]
    [Authorize(AuthRolePolicyString.AdminRole)]
    public async ValueTask<IActionResult> AddMusicData(MusicData[] musicDataList,
        MaimaiDXMusicDataHandler handler)
    {
        var user = await GetCurrentRequestUser();

        var response = await handler.AddMusicData(user, musicDataList);
        return Json(response);
    }

    [HttpGet("GetMusicData")]
    public async ValueTask<IActionResult> GetMusicData(int[] musicIdList,
        MaimaiDXMusicDataHandler handler)
    {
        var user = await GetCurrentRequestUser();

        var response = await handler.GetMusicData(user, musicIdList);
        return Json(response);
    }

    [HttpGet("GetAllMusicData")]
    public async ValueTask<IActionResult> GetAllMusicData(MaimaiDXMusicDataHandler handler)
    {
        var response = await handler.GetAllMusicData();
        return Json(response);
    }

    [HttpGet("GetRivalUserList")]
    public async ValueTask<IActionResult> GetRivalUserList(ulong userId, MaimaiDXUserRivalHandler handler)
    {
        var response = await handler.GetRivalUserList(userId);
        return Json(response);
    }

    [HttpPost("AddRival")]
    public async ValueTask<IActionResult> AddRival([FromForm] ulong userId, [FromForm] ulong rivalUserId,
        MaimaiDXUserRivalHandler handler)
    {
        var response = await handler.AddRival(userId, rivalUserId);
        return Json(response);
    }

    [HttpPost("DeleteRival")]
    public async ValueTask<IActionResult> DeleteRival([FromForm] ulong userId, [FromForm] ulong rivalUserId,
        MaimaiDXUserRivalHandler handler)
    {
        var response = await handler.DeleteRival(userId, rivalUserId);
        return Json(response);
    }

    [HttpGet("GetUserMusicDetail")]
    public async ValueTask<IActionResult> GetUserMusicDetail(ulong userId, int musicId,
        MaimaiDXUserMusicHandler handler)
    {
        var response = await handler.GetUserMusicDetail(userId, musicId);
        return Json(response);
    }

    [HttpGet("GetMusicDetailRank")]
    public async ValueTask<IActionResult> GetMusicDetailRank(int takeCount,
        int musicId,
        MaimaiDXUserMusicHandler handler)
    {
        var response = await handler.GetMusicDetailRank(musicId, takeCount);
        return Json(response);
    }
}