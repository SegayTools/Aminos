using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserUdemaes")]
    public class UserUdemae
    {
        [JsonIgnore]
        public UserRating UserRating { get; set; }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int rate;

        public int maxRate;

        public int classValue;

        public int maxClassValue;

        public uint totalWinNum;

        public uint totalLoseNum;

        public uint maxWinNum;

        public uint maxLoseNum;

        public uint winNum;

        public uint loseNum;

        public uint npcTotalWinNum;

        public uint npcTotalLoseNum;

        public uint npcMaxWinNum;

        public uint npcMaxLoseNum;

        public uint npcWinNum;

        public uint npcLoseNum;
    }
}