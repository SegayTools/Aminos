using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserPlaylogRequestVO
	{
		public ulong userId { get; set; }

		public UserPlaylog userPlaylog { get; set; }
	}
}
