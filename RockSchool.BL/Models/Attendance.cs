using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Models;

public class Attendance
{
    public Guid AttendanceId { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public AttendanceStatus Status { get; set; }
    public string StatusReason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? GroupId { get; set; }
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }
    public int RoomId { get; set; }
    public virtual RoomEntity Room { get; set; }
    public string Comment { get; set; }
    public bool IsTrial { get; set; }
    public bool IsCompleted { get; set; }
}