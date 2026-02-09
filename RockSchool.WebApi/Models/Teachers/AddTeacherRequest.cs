namespace RockSchool.WebApi.Models.Teachers;

public class AddTeacherRequest
{
    public TeacherInfo Teacher { get; set; }
    public WorkingPeriodInfo[] WorkingPeriods { get; set; }
}