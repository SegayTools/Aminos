using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.General.Tables;

[Index(nameof(Id), IsUnique = true)]
[Table("General.GameplayLogs")]
public class GameplayLog
{
    [Key]
    public int Id { get; set; }

    public DateTime Time { get; set; }

    public string GameId { get; set; }
}