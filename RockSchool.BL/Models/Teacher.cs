using RockSchool.Data.Entities;

namespace RockSchool.BL.Models;

public class Teacher
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Sex { get; set; }
    public long Phone { get; set; }
    public virtual UserEntity? User { get; set; }

    public int? BranchId { get; set; }
    public virtual Branch? Branch { get; set; }
    public int AgeLimit { get; set; }
    public bool AllowGroupLessons { get; set; }
    public bool AllowBands { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Discipline>? Disciplines { get; set; }

    public virtual ICollection<WorkingPeriod>? WorkingPeriods { get; set; }

    public virtual ICollection<ScheduledWorkingPeriod>? ScheduledWorkingPeriods { get; set; }

    public virtual ICollection<Band>? Bands { get; set; }

    public int[]? DisciplineIds { get; set; }
}