﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Aminos.Core.Models.Title.SDEZ.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_UserItems")]
public class UserItem
{
    [JsonIgnore]
    [Key]
    public ulong Id { get; set; }

    public ItemKind itemKind { get; set; }

    public int itemId { get; set; }

    public int stock { get; set; }

    public bool isValid { get; set; }
}