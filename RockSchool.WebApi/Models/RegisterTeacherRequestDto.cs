using RockSchool.BL.Models;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Models;

public class RegisterTeacherRequestDto
{
    public TeacherInfo Teacher { get; set; }
    public WorkingPeriod[] WorkingPeriods { get; set; }
}