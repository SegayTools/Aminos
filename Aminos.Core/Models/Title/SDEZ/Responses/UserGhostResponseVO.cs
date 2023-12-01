using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserGhostResponseVO
	{
		public ulong userId { get; set; }

		public UserGhost[] userGhostList { get; set; }
	}
}
