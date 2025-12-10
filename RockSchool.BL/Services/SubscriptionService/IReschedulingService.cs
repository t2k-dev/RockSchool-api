using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IReschedulingService
    {
        Task<Attendance> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate);
        Task UpdateSchedules(Guid subscriptionId, DateTime startingDate, Schedule[] newSchedules);
    }
}
