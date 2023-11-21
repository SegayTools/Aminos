namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserLoginResponseVO
	{
		public int returnCode { get; set; }

		public string lastLoginDate { get; set; }

		public int loginCount { get; set; }

		public int consecutiveLoginCount { get; set; }

		public ulong loginId { get; set; }

		public string Bearer { get; set; }
	}
}
