using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserFavoriteItemResponseVO
	{
		public ulong userId { get; set; }

		public int kind { get; set; }

		public ulong nextIndex { get; set; }

		public UserFavoriteItem[] userFavoriteItemList { get; set; }
	}
}
