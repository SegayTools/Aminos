using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Models.Title.SDEZ.Requests
{
	public class UserAllRequestVO
	{
		public ulong userId;

		public ulong playlogId;

		public bool isEventMode;

		public bool isFreePlay;

		public UserAll upsertUserAll;
	}
}
