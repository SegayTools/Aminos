using Aminos.Core.Models.General;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXCollectionDataHandler))]
public class MaimaiDXCollectionDataHandler
{
    private readonly ILogger<MaimaiDXCollectionDataHandler> logger;
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXCollectionDataHandler(MaimaiDXDB maimaiDxDB, ILogger<MaimaiDXCollectionDataHandler> logger)
    {
        this.maimaiDxDB = maimaiDxDB;
        this.logger = logger;
    }

    public async ValueTask<CommonApiResponse> GetAllCollectionData()
    {
        return new CommonApiResponse<AllCollectionData>(true, new AllCollectionData
        {
            FrameDatas = await maimaiDxDB.FrameDatas.ToArrayAsync(),
            IconDatas = await maimaiDxDB.IconDatas.ToArrayAsync(),
            TitleDatas = await maimaiDxDB.TitleDatas.ToArrayAsync(),
            PlateDatas = await maimaiDxDB.PlateDatas.ToArrayAsync(),
        });
    }
}