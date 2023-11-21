using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserRegions")]
    public class UserRegion
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int regionId;

        public int playCount;

        public string created;
    }
}