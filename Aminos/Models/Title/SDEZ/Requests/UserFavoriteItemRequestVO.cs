namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserFavoriteItemRequestVO
	{
		public ulong userId;

		public int kind;

		public ulong nextIndex;

		public int maxCount = 100;

		public bool isAllFavoriteItem;
	}
}
