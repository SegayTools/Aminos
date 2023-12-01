using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserPlaylogRequestVO
	{
		public ulong userId { get; set; }

		public UserPlaylog userPlaylog { get; set; }
	}
}
