using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(id))]
    [Table("MaimaiDX_GameRankings")]
    public class GameRanking
    {
        [Key]
        public long id;

        public long point;

        public string userName;
    }
}