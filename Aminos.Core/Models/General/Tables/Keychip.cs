using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.General.Tables
{
	[Table("General.Keychips")]
	public class Keychip
	{
		[JsonIgnore]
		public virtual UserAccount Owner { get; set; }

		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public bool Enable { get; set; }

		public DateTime RegisterDate { get; set; }

		public DateTime LastAccessDate { get; set; }
	}
}
