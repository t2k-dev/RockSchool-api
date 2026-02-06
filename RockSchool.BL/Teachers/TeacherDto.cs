using RockSchool.Domain.Entities;

namespace RockSchool.BL.Teachers;

public class TeacherDto
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Sex { get; set; }
    public long Phone { get; set; }
    public int BranchId { get; set; }
    public int  AgeLimit { get; set; }
    public bool AllowGroupLessons { get; set; }
    public bool AllowBands { get; set; }
    public int[] DisciplineIds { get; set; }
    public WorkingPeriod[] WorkingPeriods { get; set; }
}