using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserRatingResponseVO
	{
		public ulong userId { get; set; }

		public UserRating userRating { get; set; }
	}
}
