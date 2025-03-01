using RockSchool.Data.Entities;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherFormDto
    {
        public TeacherInfo Teacher { get; set; }
        public WorkingPeriodEntity Type { get; set; }
    }
}
