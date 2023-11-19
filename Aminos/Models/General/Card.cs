﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.General
{
	[Index(nameof(ExtId), IsUnique = true)]
	[Index(nameof(Luid), IsUnique = true)]
	public class Card
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string ExtId { get; set; }
		public string Luid { get; set; }
		public DateTime RegisterTime { get; set; }
		public DateTime AccessTime { get; set; }

		public string AccessCode => Luid;
		public long AimeId => int.Parse(ExtId);
	}
}