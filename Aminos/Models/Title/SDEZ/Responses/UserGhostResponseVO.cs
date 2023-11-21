using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserGhostResponseVO
	{
		public ulong userId { get; set; }

		public UserGhost[] userGhostList { get; set; }
	}
}
