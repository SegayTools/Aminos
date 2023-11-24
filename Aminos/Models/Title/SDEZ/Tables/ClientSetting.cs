using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("Client_Settings")]
	public class ClientSetting
	{
		[Key]
		[JsonPropertyName("clientId")]
		public string Id { get; set; }

        public int placeId { get; set; }

        public string placeName { get; set; }

        public int regionId { get; set; }

        public string regionName { get; set; }

        public string bordId { get; set; }

        public int romVersion { get; set; }

        public bool isDevelop { get; set; }

        public bool isAou { get; set; }
    }
}