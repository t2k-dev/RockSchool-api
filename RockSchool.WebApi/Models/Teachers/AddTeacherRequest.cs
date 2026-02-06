using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Teachers;

public class AddTeacherRequest
{
    public TeacherInfo Teacher { get; set; }
    public WorkingPeriod[] WorkingPeriods { get; set; }
}