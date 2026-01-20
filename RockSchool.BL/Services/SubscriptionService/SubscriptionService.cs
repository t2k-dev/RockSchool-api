using Azure.Core;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class SubscriptionService(
        SubscriptionRepository subscriptionRepository,
        ScheduleRepository scheduleRepository,
        AttendanceRepository attendanceRepository,
        IAttendanceService attendanceService,
        INoteService noteService,
        IScheduleService scheduleService)
        : ISubscriptionService
    {
        public async Task<Guid> AddSubscriptionAsync(Subscription subscription)
        {
            var subscriptionEntity = subscription.ToEntity();

            await subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
        }

        public async Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, Schedule[] schedules)
        {
            var isGroup = studentIds.Length > 1;
            var groupId = isGroup ? Guid.NewGuid() : (Guid?)null;

            var templateAttendances = ScheduleHelper.GenerateAttendances(subscriptionDetails, schedules, isGroup);

            foreach (var studentId in studentIds)
            {
                // Subscription
                var subscription = new Subscription
                {
                    TeacherId = subscriptionDetails.TeacherId,
                    DisciplineId = subscriptionDetails.DisciplineId,
                    StartDate = subscriptionDetails.StartDate,
                    StudentId = studentId,
                    AttendanceCount = subscriptionDetails.AttendanceCount,
                    AttendancesLeft = subscriptionDetails.AttendanceCount,
                    AttendanceLength = subscriptionDetails.AttendanceLength,
                    BranchId = subscriptionDetails.BranchId,
                    GroupId = groupId,
                    PaymentId = null,
                    TrialStatus = null,
                    Status = SubscriptionStatus.Draft,
                    StatusReason = null,
                    SubscriptionType = !isGroup ? SubscriptionType.Lesson : SubscriptionType.GroupLesson,
                };

                var newSubscriptionId = await AddSubscriptionAsync(subscription);

                // Schedules
                foreach (var schedule in schedules)
                {
                    schedule.SubscriptionId = newSubscriptionId;
                    await scheduleService.AddScheduleAsync(schedule);
                }

                // Attendances
                var attendancesByStudent = templateAttendances.Select(templateAttendance => new Attendance
                {
                    DisciplineId = templateAttendance.DisciplineId,
                    BranchId = templateAttendance.BranchId,
                    StartDate = templateAttendance.StartDate,
                    EndDate = templateAttendance.EndDate,
                    Status = templateAttendance.Status,
                    StatusReason = templateAttendance.StatusReason,
                    GroupId = templateAttendance.GroupId,
                    TeacherId = templateAttendance.TeacherId,
                    SubscriptionId = newSubscriptionId,
                    StudentId = studentId,
                    RoomId = templateAttendance.RoomId,
                }).ToArray();

                await attendanceService.AddAttendancesAsync(attendancesByStudent);
            }
        }

        public async Task<Subscription?> GetAsync(Guid subscriptionId)
        {
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            return subscription?.ToModel();
        }

        public async Task<Subscription[]> GetSubscriptionsByStudentId(Guid studentId)
        {
            var subscriptions = await subscriptionRepository.GetSubscriptionsByStudentIdAsync(studentId);
            return subscriptions.ToModel();
        }

        public async Task<Subscription[]?> GetSubscriptionByGroupIdAsync(Guid groupId)
        {
            var subscriptions = await subscriptionRepository.GetByGroupIdAsync(groupId);
            return subscriptions.ToModel();
        }

        public async Task<Subscription[]?> GetSubscriptionsByTeacherId(Guid teacherId)
        {
            var subscriptions = await subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
            return subscriptions?.ToModel();
        }

        public async Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            var attendances = await attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MaxBy(a => a.StartDate);

            var schedules = await scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var orderedSchedules = schedules.ToModel()
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, orderedSchedules);
            
            return availableSlot;
        }

        public async Task DecreaseAttendancesLeftCount(Guid subscriptionId)
        {
            var subscriptionEntity = await subscriptionRepository.GetAsync(subscriptionId);
            subscriptionEntity.AttendancesLeft -= 1;

            if (subscriptionEntity.AttendancesLeft == 0)
            {
                subscriptionEntity.Status = SubscriptionStatus.Completed;
            }

            if (subscriptionEntity.AttendancesLeft == 1)
            {
                await noteService.AddNoteAsync(subscriptionEntity.BranchId, $"Последнее занятие для ", null);
            }

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }

        public async Task LinkPaymentToSubscription(Guid subscriptionId, Guid paymentId)
        {
            /*var subscriptionEntity = await subscriptionRepository.GetAsync(subscriptionId);

            subscriptionEntity.Ten = paymentId;
            subscriptionEntity.Status = SubscriptionStatus.Active;

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);*/
        }
    }
}
