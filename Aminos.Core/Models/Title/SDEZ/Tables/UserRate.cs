using System.Text.Json.Serialization;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[MessagePackObject]
public class UserRate
{
    [Key(0)]
    public int musicId { get; set; }

    [Key(1)]
    public int level { get; set; }

    [Key(2)]
    public uint romVersion { get; set; }

    [Key(3)]
    public uint achievement { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="levelDiff">难度定数，比如13.4</param>
    /// <param name="achievement">曲子成绩，比如1002857</param>
    /// <returns></returns>
    public static float CalculateRating(float levelDiff, uint achievement)
    {
        var floatAchv = achievement / 10000d;

        var rankCoefficient = floatAchv switch
        {
            >= 100.5 => 22.4,
            >= 100 => 21.6,
            >= 99.5 => 21.1,
            >= 99.0 => 20.8,
            >= 98.0 => 20.3,
            >= 97.0 => 20.0,
            >= 94.0 => 16.8,
            >= 90.0 => 13.6,
            >= 80.0 => 8,
            >= 75.0 => 7.5,
            >= 70.0 => 7,
            >= 60.0 => 6,
            >= 50.0 => 5,
            >= 40.0 => 4,
            >= 30.0 => 3,
            >= 20.0 => 2,
            >= 10.0 => 1,
            >= 0.0 => 0
        };

        return (int) (levelDiff * floatAchv * rankCoefficient / 100.0f);
    }
}