using System.ComponentModel.DataAnnotations;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Entities;

public class NoteEntity
{
    [Key]
    public Guid NoteId { get; set; }
    public int BranchId { get; set; }
    public virtual BranchEntity Branch { get; set; }
    public string? Description { get; set; }
    public NoteStatus Status { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? ActualCompleteDate { get; set; }
}