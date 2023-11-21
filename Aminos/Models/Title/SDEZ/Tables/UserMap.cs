using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserMaps")]
    public class UserMap
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int mapId;

        public uint distance;

        public bool isLock;

        public bool isClear;

        public bool isComplete;
    }
}