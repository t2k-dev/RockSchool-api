using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class ReschedulingService : IReschedulingService
    {
        private readonly IAttendanceService _attendanceService;

        public ReschedulingService(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public async Task<AttendanceDto> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate)
        {
            // Update current attendance
            var attendance = await _attendanceService.GetAttendanceAsync(attendanceId);
            attendance.Status = AttendanceStatus.CanceledByStudent;

            await _attendanceService.UpdateAttendanceAsync(attendance);

            // Create Attendance
            var newAttendance = new AttendanceDto
            {
                StudentId = attendance.StudentId,
                TeacherId = attendance.TeacherId,
                DisciplineId = attendance.DisciplineId,
                StartDate = startDate,
                Status = AttendanceStatus.New,
                SubscriptionId = attendance.SubscriptionId,
            };

            await _attendanceService.AddAttendanceAsync(newAttendance);

            // UpdateSubscription

            return newAttendance;
        }
    }
}
