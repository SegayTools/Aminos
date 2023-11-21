using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_GameEvents")]
    public class GameEvent
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public int type;

        public string startDate;

        public string endDate;

        [JsonIgnore]
        public bool enable;
    }
}
