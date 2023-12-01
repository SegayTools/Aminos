﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserMaps")]
	public class UserMap
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public int mapId { get; set; }

		public uint distance { get; set; }

		public bool isLock { get; set; }

		public bool isClear { get; set; }

		public bool isComplete { get; set; }
	}
}