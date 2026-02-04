using RockSchool.BL.Helpers;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class ReschedulingService(
        IAttendanceService attendanceService,
        ISubscriptionService subscriptionService,
        IScheduleService scheduleService)
        : IReschedulingService
    {
        public async Task<Attendance> RescheduleAttendanceByStudent(Guid attendanceId, DateTime startDate)
        {
            throw new NotImplementedException();
            /*
            
            // Update current attendance
            var attendance = await attendanceService.GetAttendanceAsync(attendanceId);
            attendance.Status = AttendanceStatus.CanceledByStudent;
            */
            /*var subscription = await subscriptionService.GetAsync(attendance.SubscriptionId);

            await attendanceService.UpdateAttendanceAsync(attendance);

            // Create Attendance
            var endDate = startDate.AddMinutes(subscription.AttendanceLength);
            var newAttendance = new Attendance
            {
                Status = AttendanceStatus.New,
                StartDate = startDate,
                EndDate = endDate,

                TeacherId = attendance.TeacherId,
                DisciplineId = attendance.DisciplineId,
                RoomId = attendance.RoomId,
                BranchId = attendance.BranchId,
                GroupId = attendance.GroupId,
                AttendanceType = attendance.AttendanceType,
            };

            await attendanceService.AddAttendanceAsync(newAttendance);

            return newAttendance;*/
            return null;
        }

        public async Task UpdateSchedules(Guid subscriptionId, DateTime startingDate, Schedule[] newSchedules)
        {
            throw new NotImplementedException();
            /*
            // Update Schedules
            await scheduleService.DeleteBySubscriptionAsync(subscriptionId);

            foreach (var schedule in newSchedules)
            {
                schedule.SubscriptionId = subscriptionId;
            }
            await scheduleService.AddSchedulesAsync(newSchedules);

            // Update Attendances
            var orderedSchedules = newSchedules
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var attendances = await attendanceService.GetAttendancesBySubscriptionId(subscriptionId);
            if (attendances != null)
            {
                var attendancesToUpdate = attendances.Where(a => a.StartDate > startingDate);

                foreach (var attendance in attendancesToUpdate)
                {
                    var slot = ScheduleHelper.GetNextAvailableSlot(startingDate, orderedSchedules);

                    await attendanceService.UpdateDateAndLocationAsync(attendance.AttendanceId, slot.StartDate, slot.EndDate, slot.RoomId);

                    startingDate = slot.EndDate;
                }
            }*/
        }
    }
}