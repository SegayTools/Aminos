using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserChargeResponseVO
	{
		public ulong userId { get; set; }

		public long length { get; set; }

		public UserCharge[] userChargeList { get; set; }
	}
}
