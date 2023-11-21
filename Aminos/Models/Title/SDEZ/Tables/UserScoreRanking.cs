using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserScoreRankings")]
    public class UserScoreRanking
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int tournamentId;

        public long totalScore;

        public int ranking;

        public string rankingDate;
    }
}