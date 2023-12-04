using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.General.Tables;

[Index(nameof(Id), IsUnique = true)]
[Table("General.Announcements")]
public class Announcement
{
    [Key]
    public int Id { get; set; }

    public DateTime Time { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    [JsonInclude]
    public string Author => UserAccount.Name;

    [JsonIgnore]
    public virtual UserAccount UserAccount { get; set; }
}