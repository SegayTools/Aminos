using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_MapBoundMusicDatas")]
public class MapBoundMusicData
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public string Name { get; set; }

    [Column(nameof(MusicDatas))]
    public byte[] __musicDataIds { get; set; }

    [NotMapped]
    [MaxLength(256)]
    public int[] MusicDatas
    {
        get => __musicDataIds is null ? new int[0] : MessagePackSerializer.Deserialize<int[]>(__musicDataIds);
        set => __musicDataIds = MessagePackSerializer.Serialize(value);
    }
}