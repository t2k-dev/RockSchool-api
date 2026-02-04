using Azure.Core;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class SubscriptionService(
        SubscriptionRepository subscriptionRepository,
        ScheduleRepository scheduleRepository,
        AttendanceRepository attendanceRepository,
        SubscriptionsAttendancesRepository subscriptionsAttendancesRepository,
        IAttendanceService attendanceService,
        INoteService noteService,
        IScheduleService scheduleService)
        : ISubscriptionService
    {
        public async Task<Guid> AddSubscriptionAsync(Subscription subscription)
        {

            await subscriptionRepository.AddSubscriptionAsync(subscription);

            return subscription.SubscriptionId;
        }

        public async Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, Schedule[] schedules)
        {
            throw new NotImplementedException();
            /*
            var isGroup = studentIds.Length > 1;
            var groupId = isGroup ? Guid.NewGuid() : (Guid?)null;

            // Step 1: Create Subscriptions for each student
            var subscriptionIds = new List<Guid>();
            foreach (var studentId in studentIds)
            {
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
                    TrialStatus = null,
                    Status = SubscriptionStatus.Draft,
                    StatusReason = null,
                    SubscriptionType = !isGroup ? SubscriptionType.Lesson : SubscriptionType.GroupLesson,
                    TariffId = subscriptionDetails.TariffId,
                    Price = subscriptionDetails.Price,
                    FinalPrice = subscriptionDetails.FinalPrice,
                    AmountOutstanding = subscriptionDetails.FinalPrice,
                };

                var newSubscriptionId = await AddSubscriptionAsync(subscription);
                subscriptionIds.Add(newSubscriptionId);

                // Step 2: Create Schedules for each subscription
                foreach (var schedule in schedules)
                {
                    var scheduleClone = new Schedule
                    {
                        SubscriptionId = newSubscriptionId,
                        WeekDay = schedule.WeekDay,
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime,
                        RoomId = schedule.RoomId
                    };
                    await scheduleService.AddScheduleAsync(scheduleClone);
                }
            }

            // Step 3: Generate and create Attendances (shared for group lessons, individual for private)
            var attendances = ScheduleHelper.GenerateAttendances(subscriptionDetails, schedules, isGroup);
            var attendanceIds = new List<Guid>();
            
            foreach (var attendance in attendances)
            {
                var attendanceId = await attendanceService.AddAttendanceAsync(attendance);
                attendanceIds.Add(attendanceId);
            }

            // Step 4: Create Attendees junction table entries
            foreach (var subscriptionId in subscriptionIds)
            {
                foreach (var attendanceId in attendanceIds)
                {
                    var attendee = new Attendee()
                    {
                        SubscriptionAttendanceId = Guid.NewGuid(),
                        SubscriptionId = subscriptionId,
                        AttendanceId = attendanceId,
                        Status = AttendanceStatus.New, // Initial status for the relationship
                        StatusReason = null
                    };

                    await subscriptionsAttendancesRepository.AddAsync(attendee);
                }
            }*/
        }

        public async Task<Subscription?> GetAsync(Guid subscriptionId)
        {
            return await subscriptionRepository.GetAsync(subscriptionId);
        }

        public async Task<Subscription[]> GetSubscriptionsByStudentId(Guid studentId)
        {
            return await subscriptionRepository.GetSubscriptionsByStudentIdAsync(studentId);
        }

        public async Task<Subscription[]?> GetSubscriptionByGroupIdAsync(Guid groupId)
        {
            return await subscriptionRepository.GetByGroupIdAsync(groupId);
        }

        public async Task<Subscription[]?> GetSubscriptionsByTeacherId(Guid teacherId)
        {
            return await subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
        }

        public async Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            var attendances = await attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MaxBy(a => a.StartDate);

            var schedules = await scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var orderedSchedules = schedules
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, orderedSchedules);
            
            return availableSlot;
        }

        public async Task DecreaseAttendancesLeftCount(Guid subscriptionId)
        {
            throw new NotImplementedException();
            /*
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

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);*/
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
            /*
            var existingEntity = await subscriptionRepository.GetAsync(subscription.SubscriptionId);

            if (existingEntity == null)
                throw new NullReferenceException("SubscriptionEntity not found.");

            ModifySubscriptionAttributes(subscription, existingEntity);

            await subscriptionRepository.UpdateSubscriptionAsync(existingEntity);*/
        }

        private static void ModifySubscriptionAttributes(Subscription updatedSubscription, Subscription existingEntity)
        {
            throw new NotImplementedException();
            /*
            if (updatedSubscription.Status != default)
                existingEntity.Status = updatedSubscription.Status;

            if (updatedSubscription.StatusReason != default)
                existingEntity.StatusReason = updatedSubscription.StatusReason;

            if (updatedSubscription.StartDate != default)
                existingEntity.StartDate = updatedSubscription.StartDate;

            existingEntity.AmountOutstanding = updatedSubscription.AmountOutstanding;*/
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
