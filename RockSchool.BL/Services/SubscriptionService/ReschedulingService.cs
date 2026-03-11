using RockSchool.BL.Attendances;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Schedules;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Subscriptions;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class ReschedulingService(
        IAttendanceService attendanceService,
        IAttendanceRepository attendanceRepository,
        ISubscriptionService subscriptionService,
        ISubscriptionRepository subscriptionRepository,
        IScheduleService scheduleService,
        IScheduleRepository scheduleRepository,
        IBandRepository bandRepository,
        IUnitOfWork unitOfWork)
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

        public async Task UpdateScheduleBySubscription(Guid subscriptionId, DateTime startingDate, ScheduleSlotDto[] scheduleSlotDtos)
        {
            // Assign new schedule to subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            if (subscription.ScheduleId.HasValue)
            {
                await scheduleRepository.DeleteAsync(subscription.ScheduleId.Value);
            }

            var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);

            subscription.AssignSchedule(scheduleId);

            subscriptionRepository.Update(subscription);

            // Regenerate attendances
            var attendances = await attendanceRepository.GetByBandIdAsync(subscriptionId);
            RegenerateAttendances(attendances, startingDate, scheduleSlotDtos);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateScheduleByBand(Guid bandId, DateTime startingDate, ScheduleSlotDto[] scheduleSlotDtos)
        {
            // Assign new schedule to band
            var band = await bandRepository.GetByIdAsync(bandId);

            if (band.ScheduleId.HasValue)
            {
                await scheduleRepository.DeleteAsync(band.ScheduleId.Value);
            }

            var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);

            band.AssignSchedule(scheduleId);

            bandRepository.Update(band);

            // Regenerate attendances
            var attendances = await attendanceRepository.GetByBandIdAsync(bandId);
            RegenerateAttendances(attendances, startingDate, scheduleSlotDtos);

            await unitOfWork.SaveChangesAsync();
        }

        private void RegenerateAttendances(Attendance[] attendances, DateTime startingDate, ScheduleSlotDto[] scheduleSlotDtos)
        {
            if (attendances.Length <= 0) 
                return;

            var orderedScheduleSlots = scheduleSlotDtos
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var attendancesToUpdate = attendances.Where(a => a.StartDate > startingDate);

            foreach (var attendance in attendancesToUpdate)
            {
                var slot = AttendanceScheduleHelper.GetNextAvailableSlot(startingDate, orderedScheduleSlots);

                attendance.UpdateSchedule(slot.StartDate, slot.EndDate, slot.RoomId);
                attendanceRepository.Update(attendance);

                startingDate = slot.EndDate;
            }
        }
    }
}