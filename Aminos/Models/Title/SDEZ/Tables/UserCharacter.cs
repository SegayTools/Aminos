using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCharacters")]
    public class UserCharacter
    {
        [Key]
        public int Id { get; set; }

        public int characterId;

        public uint level;

        public uint awakening;

        public uint useCount;
    }
}