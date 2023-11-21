﻿namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserPreviewResponseVO
	{
		public ulong userId;

		public string userName;

		public bool isLogin;

		public string lastGameId;

		public string lastDataVersion;

		public string lastRomVersion;

		public string lastLoginDate;

		public string lastPlayDate;

		public int playerRating;

		public int nameplateId;

		public int iconId;

		public int trophyId;

		public int partnerId;

		public int frameId;

		public int dispRate;

		public int totalAwake;

		public int isNetMember;

		public string dailyBonusDate;

		public int headPhoneVolume;

		public bool isInherit;

		public int banState;

		public bool IsNewUser()
		{
			if (!string.IsNullOrEmpty(userName))
				return string.IsNullOrEmpty(lastPlayDate);
			return true;
		}

		public bool IsInheritUser()
		{
			return isInherit;
		}
	}
}
