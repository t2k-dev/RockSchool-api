using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class SubscriptionEntity
{
    [Key] public int SubscriptionId { get; set; }

    public bool IsGroup { get; set; }

    public int StudentId { get; set; }
    [ForeignKey(nameof(StudentId))]
    public virtual StudentEntity Student { get; set; }

    public int AttendanceCount { get; set; }

    public int AttendanceLength { get; set; }

    public DateTime StartDate { get; set; }

    public string Status { get; set; }

    public int DisciplineId { get; set; }
    [ForeignKey(nameof(DisciplineId))]
    public virtual DisciplineEntity Discipline { get; set; }

    public int? TransactionId { get; set; }

    public int TeacherId { get; set; }
    [ForeignKey(nameof(TeacherId))]
    public virtual TeacherEntity Teacher { get; set; }
    public int BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual BranchEntity Branch { get; set; }
    public bool IsTrial { get; set; }
}