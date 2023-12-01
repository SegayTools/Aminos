using Aminos.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.General.Tables
{
	[Table("General.UserAccounts")]
	public class UserAccount
	{
		[JsonIgnore]
		public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
		[JsonIgnore]
		public virtual ICollection<Keychip> Keychips { get; set; } = new List<Keychip>();

		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; }

		[JsonIgnore]
		public string PasswordHash { get; set; }

		public DateTime RegisterDate { get; set; }

		public DateTime LastLoginWebDate { get; set; }

		public AuthRolePolicy Role { get; set; }

		public string Email { get; set; }
	}
}
