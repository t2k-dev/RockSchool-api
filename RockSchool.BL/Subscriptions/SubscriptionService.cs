using Azure.Core;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.BL.Schedules;
using RockSchool.BL.Attendances;

namespace RockSchool.BL.Subscriptions
{
    public class SubscriptionService(
        ISubscriptionRepository subscriptionRepository,
        IScheduleRepository scheduleRepository,
        IAttendanceRepository attendanceRepository,
        IAttendeeRepository attendeeRepository,
        IAttendanceService attendanceService,
        INoteService noteService,
        IScheduleService scheduleService,
        IUnitOfWork unitOfWork)
        : ISubscriptionService
    {
        public async Task<Guid> AddSubscriptionAsync(Subscription subscription)
        {

            await subscriptionRepository.AddAsync(subscription);

            return subscription.SubscriptionId;
        }

        public async Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, ScheduleSlotDto[] scheduleSlotDtos)
        {
            // Step 1: Create schedule
            var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);

            // Step 2: Create Subscriptions for each student
            var groupId = studentIds.Length > 1 ? Guid.NewGuid() : (Guid?)null;

            var subscriptionStudentPairs = new List<(Guid SubscriptionId, Guid StudentId)>();
            foreach (var studentId in studentIds)
            {
                var subscription = Subscription.Create(studentId,
                    subscriptionDetails.BranchId,
                    subscriptionDetails.StartDate,
                    subscriptionDetails.AttendanceCount,
                    subscriptionDetails.AttendanceLength,
                    groupId == null ? SubscriptionType.Lesson : SubscriptionType.GroupLesson,
                    subscriptionDetails.Price,
                    subscriptionDetails.FinalPrice,
                    subscriptionDetails.BaseSubscriptionId,
                    subscriptionDetails.DisciplineId,
                    subscriptionDetails.TeacherId,
                    groupId
                );

                subscription.AssignSchedule(scheduleId);

                await subscriptionRepository.AddAsync(subscription);
                subscriptionStudentPairs.Add((subscription.SubscriptionId, studentId));
            }

            // Step 3: Generate and create Attendances (shared for group lessons, individual for private)
            var attendances = AttendanceScheduleHelper.Generate(
                subscriptionDetails.AttendanceCount,
                subscriptionDetails.AttendanceLength,
                subscriptionDetails.StartDate,
                subscriptionDetails.BranchId,
                subscriptionDetails.DisciplineId,
                subscriptionDetails.TeacherId,
                scheduleSlotDtos,
                groupId != null ? AttendanceType.GroupLesson : AttendanceType.Lesson,
                null,
                groupId);

            var attendanceIds = new List<Guid>();
            foreach (var attendance in attendances)
            {
                var attendanceId = await attendanceRepository.AddAsync(attendance);
                attendanceIds.Add(attendanceId);
            }

            // Step 4: Create Attendees junction table entries
            foreach (var (subscriptionId, studentId) in subscriptionStudentPairs)
            {
                foreach (var attendanceId in attendanceIds)
                {
                    var attendee = Attendee.Create(subscriptionId, attendanceId, studentId);

                    await attendeeRepository.AddAsync(attendee);
                }
            }

            await unitOfWork.SaveChangesAsync();
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
            throw new NotImplementedException();
            /*var attendances = await attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MaxBy(a => a.StartDate);

            var schedules = await scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var orderedSchedules = schedules
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, orderedSchedules);
            
            return availableSlot;*/
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

        public async Task ReduceAttendanceCount(Guid subscriptionId)
        {
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            
            subscription.ReduceAttendanceCount();

            subscriptionRepository.Update(subscription);
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
