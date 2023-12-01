﻿using Aminos.Core.Models.Title.SDEZ.Enums;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserExtends")]
	public class UserExtend
	{
		[Key]
		public ulong Id { get; set; }

		public int selectMusicId { get; set; }

		public int selectDifficultyId { get; set; }

		public int categoryIndex { get; set; }

		public int musicIndex { get; set; }

		public int extraFlag { get; set; }

		public int selectScoreType { get; set; }

		public ulong extendContentBit { get; set; }

		public bool isPhotoAgree { get; set; }

		public bool isGotoCodeRead { get; set; }

		public bool selectResultDetails { get; set; }

		public int selectResultScoreViewType { get; set; }

		public SortTabID sortCategorySetting { get; set; }

		public SortMusicID sortMusicSetting { get; set; }

		public PlaystatusTabID playStatusSetting { get; set; }

		[Column(nameof(selectedCardList))]
		[MaxLength(256)]
		public string __selectedCardList { get; set; }
		[NotMapped]
		public int[] selectedCardList
		{
			get => string.IsNullOrWhiteSpace(__selectedCardList) ? new int[0] : __selectedCardList?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			set => __selectedCardList = string.Join(",", value);
		}

		[Column(nameof(encountMapNpcList))]
		[MaxLength(256)]
		public string __encountMapNpcList { get; set; }
		[NotMapped]
		public MapEncountNpc[] encountMapNpcList
		{
			get => string.IsNullOrWhiteSpace(__encountMapNpcList) ? new MapEncountNpc[0] : __encountMapNpcList?.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(x =>
			{
				var split = x.Split(",");
				return new MapEncountNpc()
				{
					musicId = int.Parse(split.ElementAtOrDefault(0) ?? "0"),
					npcId = int.Parse(split.ElementAtOrDefault(1) ?? "0")
				};
			}).ToArray();
			set => __encountMapNpcList = string.Join(";", value.Select(x => $"{x.musicId},{x.npcId}"));
		}
	}
}