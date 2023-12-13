using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_UserActivities")]
public class UserActivity
{
    [Key]
    [JsonIgnore]
    public ulong ActivityId { get; set; }

    public ulong Id { get; set; }

    public int kind { get; set; }

    public long sortNumber { get; set; }

    public int param1 { get; set; }

    public int param2 { get; set; }

    public int param3 { get; set; }

    public int param4 { get; set; }
}