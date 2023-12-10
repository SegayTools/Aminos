using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXClientTestmodeHandler))]
public class MaimaiDXClientTestmodeHandler
{
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXClientTestmodeHandler(MaimaiDXDB maimaiDxDB)
    {
        this.maimaiDxDB = maimaiDxDB;
    }

    public async ValueTask<UpsertResponseVO> UpsertClientTestmode(ClientTestmodeRequestVO request)
    {
        var testMode = request.clientTestmode;
        var exist = await maimaiDxDB.ClientTestmodes.FirstOrDefaultAsync(x => x.Id == testMode.Id);
        if (exist is not null)
            maimaiDxDB.Entry(exist).CurrentValues.SetValues(testMode);
        else
            await maimaiDxDB.ClientTestmodes.AddAsync(testMode);
        await maimaiDxDB.SaveChangesAsync();

        var response = new UpsertResponseVO();
        response.apiName = nameof(MaimaiDXClientBookkeepHandler);
        response.returnCode = 1;

        return response;
    }
}