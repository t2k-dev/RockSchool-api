using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IReschedulingService
    {
        Task<AttendanceDto> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate);
    }
}
