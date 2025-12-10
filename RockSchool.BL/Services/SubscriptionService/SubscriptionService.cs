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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly ScheduleRepository _scheduleRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly IAttendanceService _attendanceService;
        private readonly IScheduleService _scheduleService;
        private readonly INoteService _noteService;

        public SubscriptionService(SubscriptionRepository subscriptionRepository, ScheduleRepository scheduleRepository, AttendanceRepository attendanceRepository, IAttendanceService attendanceService, INoteService noteService, IScheduleService scheduleService)
        {
            _subscriptionRepository = subscriptionRepository;
            _scheduleRepository = scheduleRepository;
            _attendanceRepository = attendanceRepository;
            _attendanceService = attendanceService;
            _noteService = noteService;
            _scheduleService = scheduleService;
        }

        public async Task<Guid> AddSubscriptionAsync(Subscription subscription)
        {
            var subscriptionEntity = new SubscriptionEntity
            {
                AttendanceCount = subscription.AttendanceCount,
                AttendancesLeft = subscription.AttendancesLeft,
                AttendanceLength = subscription.AttendanceLength,
                BranchId = subscription.BranchId,
                DisciplineId = subscription.DisciplineId,
                GroupId = subscription.GroupId,
                StartDate = subscription.StartDate,
                Status = subscription.Status,
                StudentId = subscription.StudentId,
                TeacherId = subscription.TeacherId,
                TransactionId = subscription.TransactionId,
                TrialStatus = (int?)subscription.TrialStatus,
                StatusReason = subscription.StatusReason,
                
            };

            await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
        }

        public async Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, Schedule[] schedules)
        {
            var isGroup = studentIds.Length > 1;
            var groupId = isGroup ? Guid.NewGuid() : (Guid?)null;

            var templateAttendances = ScheduleHelper.GenerateTemplateAttendances(subscriptionDetails, schedules, isGroup);

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
                    TransactionId = null,
                    TrialStatus = null,
                    Status = (int)SubscriptionStatus.Active,
                    StatusReason = null,
                };

                var newSubscriptionId = await AddSubscriptionAsync(subscription);

                // Schedules
                foreach (var schedule in schedules)
                {
                    schedule.SubscriptionId = newSubscriptionId;
                    await _scheduleService.AddScheduleAsync(schedule);
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

                await _attendanceService.AddAttendancesAsync(attendancesByStudent);
            }
        }

        public async Task<Subscription?> GetAsync(Guid subscriptionId)
        {
            var subscription = await _subscriptionRepository.GetAsync(subscriptionId);
            return subscription?.ToDto();
        }

        public async Task<Subscription[]> GetSubscriptionsByStudentId(Guid studentId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByStudentIdAsync(studentId);
            return subscriptions.ToDto();
        }

        public async Task<Subscription[]?> GetSubscriptionByGroupIdAsync(Guid groupId)
        {
            var subscriptions = await _subscriptionRepository.GetByGroupIdAsync(groupId);
            return subscriptions.ToDto();
        }

        public async Task<Subscription[]?> GetSubscriptionsByTeacherId(Guid teacherId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
            return subscriptions?.ToDto();
        }

        public async Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            var attendances = await _attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MaxBy(a => a.StartDate);

            var schedules = await _scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var orderedSchedules = schedules.ToDto()
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, orderedSchedules);
            
            return availableSlot;
        }

        public async Task DecreaseAttendancesLeftCount(Guid subscriptionId)
        {
            var subscriptionEntity = await _subscriptionRepository.GetAsync(subscriptionId);
            subscriptionEntity.AttendancesLeft -= 1;

            await _subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }
    }
}
