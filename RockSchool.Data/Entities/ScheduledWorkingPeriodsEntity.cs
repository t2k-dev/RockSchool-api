using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class ScheduledWorkingPeriodEntity
{
    [Key]
    public Guid ScheduledWorkingPeriodId { get; set; }

    public Guid WorkingPeriodId { get; set; }

    public WorkingPeriodEntity WorkingPeriod { get; set; }

    public Guid TeacherId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public TeacherEntity? Teacher { get; set; }
}