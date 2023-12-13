using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Databases.Title.SDEZ;

public class MaimaiDXDB : DbContext
{
    public MaimaiDXDB(DbContextOptions<MaimaiDXDB> options) : base(options)
    {
    }

    public DbSet<GameEvent> GameEvents { get; set; }
    public DbSet<GameRanking> GameRanks { get; set; }
    public DbSet<GameCharge> GameCharges { get; set; }

    public DbSet<ClientBookkeeping> ClientBookkeepings { get; set; }
    public DbSet<ClientSetting> ClientSettings { get; set; }
    public DbSet<ClientTestmode> ClientTestmodes { get; set; }

    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<UserMusicDetail> UserMusicDetails { get; set; }
    public DbSet<User2pPlaylogDetail> User2pPlaylogDetails { get; set; }

    public DbSet<MusicData> MusicDatas { get; set; }
    public DbSet<EventData> EventDatas { get; set; }
    public DbSet<FrameData> FrameDatas { get; set; }
    public DbSet<CharaData> CharaDatas { get; set; }
    public DbSet<IconData> IconDatas { get; set; }
    public DbSet<TitleData> TitleDatas { get; set; }
    public DbSet<MapBoundMusicData> MapBoundMusicDatas { get; set; }
    public DbSet<PlateData> PlateDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
}