using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class SubscriptionEntity
{
    [Key] 
    public Guid SubscriptionId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public virtual StudentEntity Student { get; set; }

    public int AttendanceCount { get; set; }

    public int AttendanceLength { get; set; }

    public int AttendancesLeft { get; set; }

    public DateOnly StartDate { get; set; }

    public int Status { get; set; }

    public string? StatusReason { get; set; }

    public int DisciplineId { get; set; }

    [ForeignKey(nameof(DisciplineId))]
    public virtual DisciplineEntity Discipline { get; set; }

    public int? TransactionId { get; set; }

    public Guid TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual TeacherEntity Teacher { get; set; }

    public int BranchId { get; set; }

    [ForeignKey(nameof(BranchId))]
    public virtual BranchEntity Branch { get; set; }

    public int? TrialStatus { get; set; }
}