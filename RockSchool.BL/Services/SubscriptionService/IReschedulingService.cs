using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IReschedulingService
    {
        Task<Attendance> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate);
        Task UpdateScheduleBySubscription(Guid subscriptionId, DateTime startingDate, ScheduleSlotDto[] newSchedules);
        Task UpdateScheduleByBand(Guid bandId, DateTime startingDate, ScheduleSlotDto[] newSchedules);
    }
}
