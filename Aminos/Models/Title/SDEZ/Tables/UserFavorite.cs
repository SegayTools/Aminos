using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserFavorites")]
	public class UserFavorite
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public ulong userId { get; set; }

		public int itemKind { get; set; }

		[NotMapped]
		public int[] itemId
		{
			get => string.IsNullOrWhiteSpace(__itemId) ? new int[0] : __itemId.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			set => __itemId = string.Join(",", value);
		}

		[JsonIgnore]
		[Column("itemId")]
		public string __itemId { get; set; }
	}
}