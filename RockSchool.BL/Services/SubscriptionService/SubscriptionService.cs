using Azure.Core;
using RockSchool.BL.Dtos;
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

        public async Task<Guid> AddSubscriptionAsync(Subscription subscriptionDto)
        {
            var subscriptionEntity = new SubscriptionEntity
            {
                AttendanceCount = subscriptionDto.AttendanceCount,
                AttendanceLength = subscriptionDto.AttendanceLength,
                BranchId = subscriptionDto.BranchId,
                DisciplineId = subscriptionDto.DisciplineId,
                GroupId = subscriptionDto.GroupId,
                StartDate = subscriptionDto.StartDate,
                Status = subscriptionDto.Status,
                StudentId = subscriptionDto.StudentId,
                TeacherId = subscriptionDto.TeacherId,
                TransactionId = subscriptionDto.TransactionId,
                TrialStatus = (int?)subscriptionDto.TrialStatus,
                StatusReason = subscriptionDto.StatusReason,
                
            };

            await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
        }

        public async Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, ScheduleDto[] schedules)
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

        public async Task<Subscription[]> GetSubscriptionsByTeacherId(Guid teacherId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
            return subscriptions.ToDto();
        }

        public async Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            var attendances = await _attendanceRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MaxBy(a => a.StartDate);

            var schedules = await _scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var orderedSchedules = schedules.ToDto()
                .OrderBy(s => s.WeekDay)
                .ThenBy(s => s.StartTime)
                .ToArray();

            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, orderedSchedules);
            
            return availableSlot;
        }

        public async Task DeclineTrialSubscription(Guid subscriptionId, string statusReason)
        {
            var subscriptionEntity = await _subscriptionRepository.GetAsync(subscriptionId);
            subscriptionEntity.Status = (int)SubscriptionStatus.Completed;
            subscriptionEntity.TrialStatus = (int)TrialStatus.Negative;
            subscriptionEntity.StatusReason = statusReason;

            await _subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }

        public async Task AcceptTrialSubscription(Guid subscriptionId, string statusReason)
        {
            var subscriptionEntity = await _subscriptionRepository.GetAsync(subscriptionId);
            subscriptionEntity.Status = (int)SubscriptionStatus.Completed;
            subscriptionEntity.TrialStatus = (int)TrialStatus.Positive;
            subscriptionEntity.StatusReason = statusReason;

            await _subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }

        public Task<Attendance> RescheduleAttendance(Guid attendanceId, DateTime startDate)
        {
            //RescheduleAttendanceByStudent
            return null;
        }

        public async Task<Guid> AddTrialSubscriptionAsync(TrialRequestDto request)
        {
            var subscriptionDto = new Subscription
            {
                DisciplineId = request.DisciplineId,
                StudentId = request.Student.StudentId,
                AttendanceCount = 1,
                AttendanceLength = 1,
                BranchId = request.BranchId,
                GroupId = null,
                StartDate = DateOnly.FromDateTime(request.TrialDate),
                TrialStatus = TrialStatus.Created,
                TransactionId = null,
                Status = (int)SubscriptionStatus.Active,
                TeacherId = request.TeacherId
            };

            var subscriptionId = await AddSubscriptionAsync(subscriptionDto);

            var trialAttendance = new Attendance
            {
                StartDate = request.TrialDate,
                EndDate = request.TrialDate.AddHours(1),
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                Comment = string.Empty,
                DisciplineId = request.DisciplineId,
                GroupId = null,
                Status = AttendanceStatus.New,
                StatusReason = string.Empty,
                StudentId = request.Student.StudentId,
                TeacherId = request.TeacherId,
                SubscriptionId = subscriptionId,
                IsTrial = true,
            };

            await _attendanceService.AddAttendanceAsync(trialAttendance);

            await _noteService.AddNoteAsync(request.BranchId, $"Пробное занятие {request.Student.FirstName}.", request.TrialDate.ToUniversalTime());

            return subscriptionId;
        }
    }
}
