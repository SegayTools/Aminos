using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases.Title.SDEZ;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ;

[RegisterInjectable(typeof(MaimaiDXMusicDataHandler))]
public class MaimaiDXMusicDataHandler
{
    private readonly ILogger<MaimaiDXMusicDataHandler> logger;
    private readonly MaimaiDXDB maimaiDxDB;

    public MaimaiDXMusicDataHandler(MaimaiDXDB maimaiDxDB, ILogger<MaimaiDXMusicDataHandler> logger)
    {
        this.maimaiDxDB = maimaiDxDB;
        this.logger = logger;
    }

    public async ValueTask<CommonApiResponse> GetMusicData(UserAccount user, int[] musicIdList)
    {
        return new CommonApiResponse<MusicData[]>(true,
            await maimaiDxDB.MusicDatas.Where(x => musicIdList.Contains(x.Id)).ToArrayAsync());
    }

    public async ValueTask<CommonApiResponse> AddMusicData(UserAccount user, MusicData[] musicDataList)
    {
        using var disp = await maimaiDxDB.Database.BeginTransactionAsync();

        foreach (var data in musicDataList)
            try
            {
                await maimaiDxDB.MusicDatas.AddAsync(data);
            }
            catch (Exception e)
            {
                await disp.RollbackAsync();
                var trackId = logger.LogErrorAndGetTrackId(e, $"添加MusicData数据musicId={data.Id}失败");
                return new CommonApiInternalExceptionResponse(trackId);
            }

        await maimaiDxDB.SaveChangesAsync();
        await disp.CommitAsync();
        return new CommonApiResponse(true);
    }
}