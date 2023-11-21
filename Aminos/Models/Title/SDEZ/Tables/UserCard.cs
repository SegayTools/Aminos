using Aminos.Models.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCards")]
    public class UserCard
    {
        public int Id { get; set; }

        public int cardId;

        public int cardTypeId;

        public int charaId;

        public int mapId;

        public string startDate;

        public string endDate;
    }
}