using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Requests
{
	public class UserChargelogRequestVO
	{
		public ulong userId { get; set; }

		public UserChargelog userChargelog { get; set; }

		public UserCharge userCharge { get; set; }
	}
}
