using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class TeacherDto
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Sex { get; set; }
    public long Phone { get; set; }
    public virtual UserEntity? User { get; set; }

    public int? BranchId { get; set; }
    public virtual BranchDto? Branch { get; set; }
    public int AgeLimit { get; set; }
    public bool AllowGroupLessons { get; set; }

    public virtual ICollection<DisciplineDto>? Disciplines { get; set; }

    public virtual ICollection<WorkingPeriodDto>? WorkingPeriods { get; set; }

    public virtual ICollection<ScheduledWorkingPeriodDto>? ScheduledWorkingPeriods { get; set; }

    public int[]? DisciplineIds { get; set; }
}