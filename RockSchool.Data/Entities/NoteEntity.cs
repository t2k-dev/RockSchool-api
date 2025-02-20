using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class NoteEntity
{
    [Key]
    public Guid NoteId { get; set; }

    public virtual BranchEntity Branch { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
}