﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Core.Models.Title.SDEZ.Tables
{
	[Index(nameof(Id))]
	[Table("MaimaiDX_UserActivities")]
	public class UserActivity
	{
		[Key]
		[JsonIgnore]
		public ulong Id { get; set; }

		public virtual ICollection<UserAct> playList { get; set; } = new List<UserAct>();
		public virtual ICollection<UserAct> musicList { get; set; } = new List<UserAct>();
	}
}