using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class GameEventResponseVO
	{
		public int type { get; set; }

		public GameEvent[] gameEventList { get; set; }
	}
}
