﻿using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserFavoriteResponseVO
	{
		public ulong userId { get; set; }

		public UserFavorite userFavoriteData { get; set; }
	}
}
