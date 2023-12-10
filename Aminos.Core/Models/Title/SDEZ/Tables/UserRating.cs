using MessagePack;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[MessagePackObject]
public class UserRating
{
    [Key(0)]
    public int rating { get; set; }

    [Key(1)]
    public List<UserRate> ratingList { get; set; } = new();
    [Key(2)]
    public List<UserRate> newRatingList { get; set; } = new();
    [Key(3)]
    public List<UserRate> nextRatingList { get; set; } = new();
    [Key(4)]
    public List<UserRate> nextNewRatingList { get; set; } = new();

    [Key(5)]
    public virtual UserUdemae udemae { get; set; }
}