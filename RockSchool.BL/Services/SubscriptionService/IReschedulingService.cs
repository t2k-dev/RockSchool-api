using RockSchool.BL.Dtos;
using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IReschedulingService
    {
        Task<Attendance> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate);
    }
}
