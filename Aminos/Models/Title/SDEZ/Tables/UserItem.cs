using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserItems")]
    public class UserItem
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int itemKind;

        public int itemId;

        public int stock;

        public bool isValid;
    }
}