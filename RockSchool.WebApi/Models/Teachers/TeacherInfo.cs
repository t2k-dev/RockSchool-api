using System;
using RockSchool.BL.Models;
using RockSchool.Data.Entities;

namespace RockSchool.WebApi.Models.Teachers;

// TODO: Implement!
public class TeacherInfo
{
    public Guid TeacherId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int Sex { get; set; }
    public int[] Disciplines { get; set; }
    public int? UserId { get; set; }
    public int? BranchId { get; set; }
    public bool AllowGroupLessons { get; set; }
    public int AgeLimit { get; set; }
    public bool IsActive { get; set; }
    public WorkingPeriod[] WorkingPeriods { get; set; }
    public ScheduledWorkingPeriod[] ScheduledWorkingPeriods { get; set; }
}