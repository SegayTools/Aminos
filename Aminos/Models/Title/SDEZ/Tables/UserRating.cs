using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserRatings")]
    public class UserRating
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int rating;

        public UserRate[] ratingList;

        public UserRate[] newRatingList;

        public UserRate[] nextRatingList;

        public UserRate[] nextNewRatingList;
    }
}