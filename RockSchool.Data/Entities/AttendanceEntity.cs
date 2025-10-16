using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Entities;

public class AttendanceEntity
{
    [Key]
    public Guid AttendanceId { get; set; }

    public Guid StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public virtual StudentEntity Student { get; set; }

    public Guid SubscriptionId { get; set; }
    
    [ForeignKey(nameof(SubscriptionId))] 
    public virtual SubscriptionEntity Subscription { get; set; }
    
    public int DisciplineId { get; set; }
    
    [ForeignKey(nameof(DisciplineId))]
    public virtual DisciplineEntity Discipline { get; set; }
    
    public Guid TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual TeacherEntity Teacher { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? StatusReason { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid? GroupId { get; set; }
    
    [ForeignKey(nameof(BranchId))]
    public virtual BranchEntity Branch { get; set; }

    public int BranchId { get; set; }

    public int RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public virtual RoomEntity Room { get; set; }

    public string? Comment { get; set; }
    public bool IsTrial { get; set; }
}