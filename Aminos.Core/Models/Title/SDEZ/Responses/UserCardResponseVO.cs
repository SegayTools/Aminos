using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserCardResponseVO
	{
		public ulong userId { get; set; }

		public int nextIndex { get; set; }

		public UserCard[] userCardList { get; set; }
	}
}
