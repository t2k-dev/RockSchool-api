using Azure.Core;
using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
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
        private readonly INoteService _noteService;

        public SubscriptionService(SubscriptionRepository subscriptionRepository, ScheduleRepository scheduleRepository, AttendanceRepository attendanceRepository, IAttendanceService attendanceService, INoteService noteService)
        {
            _subscriptionRepository = subscriptionRepository;
            _scheduleRepository = scheduleRepository;
            _attendanceRepository = attendanceRepository;
            _attendanceService = attendanceService;
            _noteService = noteService;
        }

        public async Task<Guid> AddSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            var subscriptionEntity = new SubscriptionEntity
            {
                AttendanceCount = subscriptionDto.AttendanceCount,
                AttendanceLength = subscriptionDto.AttendanceLength,
                BranchId = subscriptionDto.BranchId,
                DisciplineId = subscriptionDto.DisciplineId,
                IsGroup = subscriptionDto.IsGroup,
                StartDate = subscriptionDto.StartDate,
                Status = subscriptionDto.Status,
                StudentId = subscriptionDto.StudentId,
                TeacherId = subscriptionDto.TeacherId,
                TransactionId = subscriptionDto.TransactionId,
                TrialStatus = (int?)subscriptionDto.TrialStatus
            };

            await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
        }

        public async Task<SubscriptionDto> GetAsync(Guid subscriptionId)
        {
            var subscription = await _subscriptionRepository.GetAsync(subscriptionId);
            return subscription.ToDto();
        }

        public async Task<SubscriptionDto[]> GetSubscriptionsByStudentId(Guid studentId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByStudentIdAsync(studentId);
            return subscriptions.ToDto();
        }

        public async Task<SubscriptionDto[]> GetSubscriptionsByTeacherId(Guid teacherId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
            return subscriptions.ToDto();
        }

        public async Task<DateTime> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            var attendances = await _attendanceRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            var lastAttendance = attendances.MinBy(a => a.StartDate);


            var schedules = await _scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
            
            var availableSlot = ScheduleHelper.GetNextAvailableSlot(lastAttendance.StartDate, schedules);
            
            return availableSlot.StartDate;
        }

        public Task<AttendanceDto> RescheduleAttendance(Guid attendanceId, DateTime startDate)
        {
            //RescheduleAttendanceByStudent
            return null;
        }

        public async Task<Guid> AddTrialSubscriptionAsync(TrialRequestDto request)
        {
            var subscriptionDto = new SubscriptionDto
            {
                DisciplineId = request.DisciplineId,
                StudentId = request.Student.StudentId,
                AttendanceCount = 1,
                AttendanceLength = 1,
                BranchId = request.BranchId,
                IsGroup = false,
                StartDate = request.TrialDate,
                TrialStatus = TrialStatus.Created,
                TransactionId = null,
                Status = (int)SubscriptionStatus.Active,
                TeacherId = request.TeacherId
            };

            var subscriptionId = await AddSubscriptionAsync(subscriptionDto);

            var trialAttendance = new AttendanceDto
            {
                StartDate = request.TrialDate,
                EndDate = request.TrialDate.AddHours(1),
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                Comment = string.Empty,
                DisciplineId = request.DisciplineId,
                IsGroup = false,
                NumberOfAttendances = 1,
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
