﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserRates")]
    public class UserRate
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public int musicId;

        public int level;

        public uint romVersion;

        public uint achievement;
    }
}