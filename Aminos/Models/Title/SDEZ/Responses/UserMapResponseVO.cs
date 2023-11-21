using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserMapResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public UserMap[] userMapList { get; set; }
	}
}
