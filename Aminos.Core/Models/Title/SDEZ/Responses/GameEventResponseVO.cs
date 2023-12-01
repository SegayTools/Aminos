using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class GameEventResponseVO
	{
		public int type { get; set; }

		public GameEvent[] gameEventList { get; set; }
	}
}
