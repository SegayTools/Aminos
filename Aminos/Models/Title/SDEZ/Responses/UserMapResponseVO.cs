﻿using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserMapResponseVO
	{
		public ulong userId;

		public long nextIndex;

		public UserMap[] userMapList;
	}
}