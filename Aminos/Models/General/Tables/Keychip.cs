using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.General.Tables
{
	[Table("General.Keychips")]
	public class Keychip
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public bool Enable { get; set; }

		public DateTime RegisterDate { get; set; }

		public DateTime LastAccessDate { get; set; }

		public virtual UserAccount Owner { get; set; }
	}
}
