using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserItemResponseVO
	{
		public ulong userId;

		public long nextIndex;

		public int itemKind;

		public UserItem[] userItemList;
	}
}
