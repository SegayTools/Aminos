using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Table("Client_Testmodes")]
	[Index(nameof(Id))]
	public class ClientTestmode
    {
		[Key]
		[JsonPropertyName("clientId")]
		public string Id { get; set; }

		public int placeId { get; set; }

		public int trackSingle { get; set; }

        public int trackMulti { get; set; }

        public int trackEvent { get; set; }

        public int totalMachine { get; set; }

        public int satelliteId { get; set; }

        public int cameraPosition { get; set; }
    }
}