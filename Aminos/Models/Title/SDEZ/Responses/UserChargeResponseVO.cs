using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserChargeResponseVO
	{
		public ulong userId { get; set; }

		public long length { get; set; }

		public UserCharge[] userChargeList { get; set; }
	}
}
