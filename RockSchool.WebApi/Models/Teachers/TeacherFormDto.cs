using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherFormDto
    {
        public TeacherInfo Teacher { get; set; }
        public WorkingPeriodDto[] WorkingPeriods { get; set; }
        public bool PeriodsChanged { get; set; }
        public bool DisciplinesChanged { get; set; }
    }
}
