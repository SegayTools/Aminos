using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserLoginBonusResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public UserLoginBonus[] userLoginBonusList { get; set; }
	}
}
