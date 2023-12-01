using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class GameChargeResponseVO
	{
		public long length { get; set; }

		public GameCharge[] gameChargeList { get; set; }
	}
}
