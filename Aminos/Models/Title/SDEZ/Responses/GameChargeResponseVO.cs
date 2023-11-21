using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class GameChargeResponseVO
	{
		public long length { get; set; }

		public GameCharge[] gameChargeList { get; set; }
	}
}
