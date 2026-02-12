using RockSchool.Domain.Entities;

namespace RockSchool.BL.Home
{
    public class HomeDetailsDto
    {
        public Branch Branch { get; set; }
        public Attendance[] Attendances { get; set; }
    }
}
