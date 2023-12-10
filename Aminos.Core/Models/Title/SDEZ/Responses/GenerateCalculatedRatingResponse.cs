namespace Aminos.Core.Models.Title.SDEZ.Responses;

public class GenerateCalculatedRatingResponse
{
    public CalculatedRating[] RatingList { get; set; }
    public CalculatedRating[] NextRatingList { get; set; }
}