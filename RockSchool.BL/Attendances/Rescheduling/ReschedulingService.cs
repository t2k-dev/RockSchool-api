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
using RockSchool.Domain.Students;

namespace RockSchool.BL.Attendances.Rescheduling
{
    public class ReschedulingService(
        IAttendanceService attendanceService,
        IAttendanceRepository attendanceRepository,
        IAttendeeRepository attendeeRepository,
        ISubscriptionService subscriptionService,
        ISubscriptionRepository subscriptionRepository,
        IScheduleService scheduleService,
        IScheduleRepository scheduleRepository,
        IBandRepository bandRepository,
        IUnitOfWork unitOfWork)
        : IReschedulingService
    {
        public async Task<Attendance> RescheduleAttendanceByAdmin(Guid attendanceId, DateTime startDate, DateTime endDate, int roomId, string statusReason)
        {
            
            var attendance = await attendanceService.GetAttendanceAsync(attendanceId);
            if (attendance == null)
                throw new InvalidOperationException($"Attendance with id {attendanceId} not found");

            // Create new attendance
            var newAttendance = Attendance.Create(
                startDate,
                endDate,
                roomId,
                attendance.BranchId,
                attendance.AttendanceType,
                attendance.BandId,
                attendance.DisciplineId,
                attendance.TeacherId,
                attendance.GroupId);

            // Cancel the existing
            attendance.CanceledByAdmin(statusReason);

            // Recreate the attendees
            foreach (var attendee in attendance.Attendees)
            {
                attendee.MarkAsCanceled();

                var newAttendee = Attendee.Create(attendee.SubscriptionId, newAttendance.AttendanceId, attendee.StudentId);
                await attendeeRepository.AddAsync(newAttendee);
            }

            await attendanceRepository.AddAsync(newAttendance);

            await unitOfWork.SaveChangesAsync();

            return newAttendance;
        }

        public async Task UpdateScheduleBySubscription(Guid subscriptionId, DateTime startingDate, ScheduleSlotDto[] scheduleSlotDtos)
        {
            // Assign new schedule to subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            if (subscription == null)
                throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

            if (subscription.ScheduleId.HasValue)
            {
                await scheduleRepository.DeleteAsync(subscription.ScheduleId.Value);
            }

            var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);

            subscription.AssignSchedule(scheduleId);

            // Regenerate attendances
            var attendances = await attendanceRepository.GetByBandIdAsync(subscriptionId);
            RegenerateAttendances(attendances, startingDate, scheduleSlotDtos);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateScheduleByBand(Guid bandId, DateTime startingDate, ScheduleSlotDto[] scheduleSlotDtos)
        {
            // Assign new schedule to band
            var band = await bandRepository.GetByIdAsync(bandId);
            if (band == null)
                throw new InvalidOperationException($"Band with id {bandId} not found");

            if (band.ScheduleId.HasValue)
            {
                await scheduleRepository.DeleteAsync(band.ScheduleId.Value);
            }

            var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);

            band.AssignSchedule(scheduleId);

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

                startingDate = slot.EndDate;
            }
        }
    }
}
