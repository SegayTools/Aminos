﻿using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserLoginBonusResponseVO
	{
		public ulong userId;

		public long nextIndex;

		public UserLoginBonus[] userLoginBonusList;
	}
}
