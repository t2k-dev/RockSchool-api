using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class ReschedulingService : IReschedulingService
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ISubscriptionService _subscriptionService;

        public ReschedulingService(IAttendanceService attendanceService, ISubscriptionService subscriptionService)
        {
            _attendanceService = attendanceService;
            _subscriptionService = subscriptionService;
        }

        public async Task<AttendanceDto> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate)
        {
            // Update current attendance
            var attendance = await _attendanceService.GetAttendanceAsync(attendanceId);
            attendance.Status = AttendanceStatus.CanceledByStudent;

            var subscription = await _subscriptionService.GetAsync(attendance.SubscriptionId);

            await _attendanceService.UpdateAttendanceAsync(attendance);

            // Create Attendance
            var endDate = startDate.AddMinutes(subscription.AttendanceLength == 1 ? 60 : 90);
            var newAttendance = new AttendanceDto
            {
                Status = AttendanceStatus.New,
                StartDate = startDate,
                EndDate = endDate,

                StudentId = attendance.StudentId,
                TeacherId = attendance.TeacherId,
                DisciplineId = attendance.DisciplineId,
                SubscriptionId = attendance.SubscriptionId,
                RoomId = attendance.RoomId,
                BranchId = attendance.BranchId,
                IsGroup = attendance.IsGroup,
                IsTrial = attendance.IsTrial,
            };

            await _attendanceService.AddAttendanceAsync(newAttendance);

            return newAttendance;
        }
    }
}