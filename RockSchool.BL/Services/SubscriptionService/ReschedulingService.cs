using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class ReschedulingService : IReschedulingService
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IScheduleService _scheduleService;

        public ReschedulingService(IAttendanceService attendanceService, ISubscriptionService subscriptionService, IScheduleService scheduleService)
        {
            _attendanceService = attendanceService;
            _subscriptionService = subscriptionService;
            _scheduleService = scheduleService;
        }

        public async Task<Attendance> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate)
        {
            // Update current attendance
            var attendance = await _attendanceService.GetAttendanceAsync(attendanceId);
            attendance.Status = AttendanceStatus.CanceledByStudent;

            var subscription = await _subscriptionService.GetAsync(attendance.SubscriptionId);

            await _attendanceService.UpdateAttendanceAsync(attendance);

            // Create Attendance
            var endDate = startDate.AddMinutes(subscription.AttendanceLength == 1 ? 60 : 90);
            var newAttendance = new Attendance
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
                GroupId = attendance.GroupId,
                IsTrial = attendance.IsTrial,
            };

            await _attendanceService.AddAttendanceAsync(newAttendance);

            return newAttendance;
        }

        public async Task UpdateSchedules(Guid subscriptionId, DateTime startingDate, Schedule[] newSchedules)
        {
            // Update Schedules
            await _scheduleService.DeleteBySubscriptionAsync(subscriptionId);

            foreach (var schedule in newSchedules)
            {
                schedule.SubscriptionId = subscriptionId;
            }
            await _scheduleService.AddSchedulesAsync(newSchedules);

            // Update Attendances
            var orderedSchedules = newSchedules
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var attendances = await _attendanceService.GetAttendancesBySubscriptionId(subscriptionId);
            if (attendances != null)
            {
                var attendancesToUpdate = attendances.Where(a => a.StartDate > startingDate);

                foreach (var attendance in attendancesToUpdate)
                {
                    var slot = ScheduleHelper.GetNextAvailableSlot(startingDate, orderedSchedules);

                    await _attendanceService.UpdateDateAndLocationAsync(attendance.AttendanceId, slot.StartDate, slot.EndDate, slot.RoomId);

                    startingDate = slot.EndDate;
                }
            }
        }
    }
}