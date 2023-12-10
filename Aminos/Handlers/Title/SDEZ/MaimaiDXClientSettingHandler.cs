using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXClientSettingHandler))]
public class MaimaiDXClientSettingHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXClientSettingHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async ValueTask<UpsertResponseVO> UpsertClientSetting(ClientSettingRequestVO request)
    {
        var setting = request.clientSetting;
        var exist = await maimaiDxDB.ClientSettings.FirstOrDefaultAsync(x => x.Id == setting.Id);
        if (exist is not null)
            maimaiDxDB.Entry(exist).CurrentValues.SetValues(setting);
        else
            await maimaiDxDB.ClientSettings.AddAsync(setting);

        await maimaiDxDB.SaveChangesAsync();

        var response = new UpsertResponseVO();
        response.apiName = nameof(MaimaiDXClientBookkeepHandler);
        response.returnCode = 1;

        return response;
    }
}