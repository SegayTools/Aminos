using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserCharacterResponseVO
	{
		public ulong userId { get; set; }

		public UserCharacter[] userCharacterList { get; set; }
	}
}
