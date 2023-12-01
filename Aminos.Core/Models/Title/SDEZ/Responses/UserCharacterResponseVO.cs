using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserCharacterResponseVO
	{
		public ulong userId { get; set; }

		public UserCharacter[] userCharacterList { get; set; }
	}
}
