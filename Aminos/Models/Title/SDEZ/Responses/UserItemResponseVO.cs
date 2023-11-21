using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserItemResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public int itemKind { get; set; }

		public UserItem[] userItemList { get; set; }
	}
}
