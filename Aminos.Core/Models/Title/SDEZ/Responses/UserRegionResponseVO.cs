using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserRegionResponseVO
	{
		public ulong userId { get; set; }

		public ulong length { get; set; }

		public UserRegion[] userRegionList { get; set; }
	}
}
