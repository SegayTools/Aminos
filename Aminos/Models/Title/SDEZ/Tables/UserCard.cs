using Aminos.Models.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserCards")]
    public class UserCard
	{
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
		[JsonIgnore]
        public ulong Id { get; set; }

        public int cardId { get; set; }

		public int cardTypeId { get; set; }

		public int charaId { get; set; }

		public int mapId { get; set; }

		public string startDate { get; set; }

		public string endDate { get; set; }
	}
}