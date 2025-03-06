using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Models;

public class RegisterTeacherRequestDto
{
    public TeacherInfo Teacher { get; set; }
    public WorkingPeriodDto[] WorkingPeriods { get; set; }
}