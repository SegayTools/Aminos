using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.General.Tables
{
	[Table("General.UserAccounts")]
	public class UserAccount
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string PasswordHash { get; set; }

		public DateTime RegisterDate { get; set; }

		public DateTime LastLoginWebDate { get; set; }

		public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

		public virtual ICollection<Keychip> Keychips { get; set; } = new List<Keychip>();
	}
}
