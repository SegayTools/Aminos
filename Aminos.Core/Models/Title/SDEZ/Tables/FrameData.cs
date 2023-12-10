using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Core.Models.Title.SDEZ.Tables;

[Index(nameof(Id))]
[Table("MaimaiDX_FrameDatas")]
public class FrameData
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Genre { get; set; }

    public string NormText { get; set; }
    
    public int Priority { get; set; }
}