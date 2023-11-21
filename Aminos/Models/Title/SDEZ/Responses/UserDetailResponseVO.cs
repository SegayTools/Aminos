using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserDetailResponseVO
	{
		public ulong userId { get; set; }

		public UserDetail userData { get; set; }

		public int banState { get; set; }
	}
}
