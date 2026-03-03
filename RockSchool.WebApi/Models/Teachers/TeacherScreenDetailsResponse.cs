using RockSchool.BL.Models.Dtos;
using RockSchool.WebApi.Models.Students;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherScreenDetailsResponse
    {
        public TeacherInfo Teacher { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceWithAttendeesDto[] Attendances { get; set; }
    }
}
