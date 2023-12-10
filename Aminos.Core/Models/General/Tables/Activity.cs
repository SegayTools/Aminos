using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aminos.Core.Models.General.Emuns;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.General.Tables;

[Index(nameof(Id), IsUnique = true)]
[Table("General.Activities")]
public class Activity
{
    [Key] public int Id { get; set; }

    public DateTime Time { get; set; }
    public string Content { get; set; }
    public ActivityType Type { get; set; }
}