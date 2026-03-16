using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Attendances.Rescheduling
{
    public interface IReschedulingService
    {
        Task<Attendance> RescheduleAttendanceByAdmin(Guid attendanceId, DateTime startDate, DateTime endDate, int roomId, string statusReason);
        Task UpdateScheduleBySubscription(Guid subscriptionId, DateTime startingDate, ScheduleSlotDto[] newSchedules);
        Task UpdateScheduleByBand(Guid bandId, DateTime startingDate, ScheduleSlotDto[] newSchedules);
    }
}
