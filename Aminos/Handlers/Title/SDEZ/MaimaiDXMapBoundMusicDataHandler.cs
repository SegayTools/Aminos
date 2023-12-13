using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXMapBoundMusicDataHandler))]
public class MaimaiDXMapBoundMusicDataHandler
{
    private readonly ILogger<MaimaiDXMapBoundMusicDataHandler> logger;
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXMapBoundMusicDataHandler(MaimaiDXDB maimaiDxDB, ILogger<MaimaiDXMapBoundMusicDataHandler> logger)
    {
        this.maimaiDxDB = maimaiDxDB;
        this.logger = logger;
    }
    
    public async ValueTask<CommonApiResponse> GetAllMapBoundMusicData()
    {
        return new CommonApiResponse<MapBoundMusicData[]>(true, await maimaiDxDB.MapBoundMusicDatas.ToArrayAsync());
    }
}