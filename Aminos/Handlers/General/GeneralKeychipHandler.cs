using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.General;

[RegisterInjectable(typeof(GeneralKeychipHandler))]
public class GeneralKeychipHandler
{
    private readonly AminosDB aminosDB;
    private readonly ILogger<GeneralKeychipHandler> logger;
    private readonly Random random = new();

    public GeneralKeychipHandler(AminosDB aminosDB, ILogger<GeneralKeychipHandler> logger)
    {
        this.aminosDB = aminosDB;
        this.logger = logger;
    }

    public async ValueTask<CommonApiResponse> Create(UserAccount userAccount, string keychipId = default)
    {
        if (userAccount is null)
            return new CommonApiResponse(false, "无法获取用户信息");

        if (string.IsNullOrWhiteSpace(keychipId))
        {
            keychipId = await GenerateNewRandomKeyChip();
            if (keychipId == default)
                return new CommonApiResponse(false, "暂时无法生成随机KeychipId, 需要手动填写KeychipId");
        }
        else
        {
            if (await aminosDB.Keychips.AnyAsync(x => x.Id == keychipId))
                return new CommonApiResponse(false, "Keychip已被注册");
        }

        var newKeychip = new Keychip
        {
            RegisterDate = DateTime.Now,
            Enable = true,
            Id = keychipId,
            Name = string.Empty
        };

        userAccount.Keychips.Add(newKeychip);

        try
        {
            await aminosDB.SaveChangesAsync();
            return new CommonApiResponse<Keychip>(true, newKeychip);
        }
        catch (Exception e)
        {
            var trackId = logger.LogErrorAndGetTrackId(e, "数据库添加新的Keychip失败");
            return new CommonApiInternalExceptionResponse(trackId);
        }
    }

    public async ValueTask<CommonApiResponse> Update(UserAccount userAccount, string keychipId, string newName,
        bool newEnable)
    {
        if (userAccount.Keychips.FirstOrDefault(x => x.Id == keychipId) is not Keychip keychip)
            return new CommonApiResponse(false, "此用户不存在此keychip");

        keychip.Name = newName;
        keychip.Enable = newEnable;

        await aminosDB.SaveChangesAsync();
        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> Delete(UserAccount userAccount, string keychipId)
    {
        if (userAccount is null)
            return new CommonApiResponse(false, "无法获取用户信息");

        if (userAccount.Keychips.FirstOrDefault(x => x.Id == keychipId) is Keychip keychip)
            try
            {
                aminosDB.Entry(keychip).State = EntityState.Deleted;
                await aminosDB.SaveChangesAsync();

                return new CommonApiResponse(true);
            }
            catch (Exception e)
            {
                var trackId = logger.LogErrorAndGetTrackId(e, "尝试删除Keychip失败");
                return new CommonApiInternalExceptionResponse(trackId);
            }

        return new CommonApiResponse(false, "此用户不存在此keychip");
    }

    public ValueTask<CommonApiResponse> List(UserAccount userAccount)
    {
        if (userAccount is null)
            return ValueTask.FromResult(new CommonApiResponse(false, "无法获取用户信息"));

        return ValueTask.FromResult<CommonApiResponse>(
            new CommonApiResponse<IEnumerable<Keychip>>(true, userAccount.Keychips.ToArray()));
    }

    private async ValueTask<string> GenerateNewRandomKeyChip()
    {
        string randHex(int length)
        {
            const string chars = "0123456789ABCDEF";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string randDigit(int length)
        {
            const string digits = "0123456789";
            return new string(Enumerable.Repeat(digits, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //A{??}E-???????
        var retryCount = 100;
        while (retryCount > 0)
        {
            var keychip = $"A{randDigit(2)}E{randHex(7)}";
            if (!await aminosDB.Keychips.AnyAsync(x => x.Id == keychip))
                return keychip;
            retryCount--;
        }

        return default;
    }
}