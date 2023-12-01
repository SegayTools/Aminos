using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserItemResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public int itemKind { get; set; }

		public UserItem[] userItemList { get; set; }
	}
}
