using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserFavoriteItemResponseVO
	{
		public ulong userId;

		public int kind;

		public ulong nextIndex;

		public UserFavoriteItem[] userFavoriteItemList;
	}
}
