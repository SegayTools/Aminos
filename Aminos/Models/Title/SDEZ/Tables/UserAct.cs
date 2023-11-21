using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserActs")]
    public class UserAct
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public int kind;

        public long sortNumber;

        public int param1;

        public int param2;

        public int param3;

        public int param4;

        public int userId;
    }
}