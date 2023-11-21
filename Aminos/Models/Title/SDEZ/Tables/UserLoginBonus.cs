using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserLoginBonuss")]
    public class UserLoginBonus
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int bonusId;

        public uint point;

        public bool isCurrent;

        public bool isComplete;
    }
}