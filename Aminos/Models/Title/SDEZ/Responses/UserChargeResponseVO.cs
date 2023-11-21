using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserChargeResponseVO
	{
		public ulong userId;

		public long length;

		public UserCharge[] userChargeList;
	}
}
