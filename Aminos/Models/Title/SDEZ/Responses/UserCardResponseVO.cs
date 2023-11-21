using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserCardResponseVO
	{
		public ulong userId { get; set; }

		public int nextIndex { get; set; }

		public UserCard[] userCardList { get; set; }
	}
}
