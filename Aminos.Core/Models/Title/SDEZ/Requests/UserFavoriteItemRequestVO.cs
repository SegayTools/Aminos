namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserFavoriteItemRequestVO
	{
		public ulong userId { get; set; }

		public int kind { get; set; }

		public ulong nextIndex { get; set; }

		public int maxCount { get; set; } = 100;

		public bool isAllFavoriteItem { get; set; }
	}
}
