using RockSchool.BL.Models;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherFormDto
    {
        public TeacherInfo Teacher { get; set; }
        public WorkingPeriod[] WorkingPeriods { get; set; }
        public bool PeriodsChanged { get; set; }
        public bool DisciplinesChanged { get; set; }
    }
}
