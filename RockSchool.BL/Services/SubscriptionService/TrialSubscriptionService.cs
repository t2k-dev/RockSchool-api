using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class TrialSubscriptionService : ITrialSubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly IAttendanceService _attendanceService;
        private readonly INoteService _noteService;
        private readonly ISubscriptionService _subscriptionService;

        public TrialSubscriptionService(SubscriptionRepository subscriptionRepository, IAttendanceService attendanceService, INoteService noteService, ISubscriptionService subscriptionService)
        {
            _subscriptionRepository = subscriptionRepository;
            _attendanceService = attendanceService;
            _noteService = noteService;
            _subscriptionService = subscriptionService;
        }

        public async Task CompleteTrial(Guid subscriptionId, TrialStatus trialStatus, string statusReason)
        {
            var subscriptionEntity = await _subscriptionRepository.GetAsync(subscriptionId);

            subscriptionEntity.AttendancesLeft -= 1;
            subscriptionEntity.Status = (int)SubscriptionStatus.Completed;
            subscriptionEntity.TrialStatus = (int)trialStatus;
            subscriptionEntity.StatusReason = statusReason;

            await _subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }

        public async Task<Guid> AddTrialSubscription(TrialRequestDto request)
        {
            var subscriptionDto = new Subscription
            {
                DisciplineId = request.DisciplineId,
                StudentId = request.Student.StudentId,
                AttendanceCount = 1,
                AttendanceLength = 1,
                AttendancesLeft = 1,
                BranchId = request.BranchId,
                GroupId = null,
                StartDate = DateOnly.FromDateTime(request.TrialDate),
                TrialStatus = TrialStatus.Created,
                TransactionId = null,
                Status = (int)SubscriptionStatus.Active,
                TeacherId = request.TeacherId
            };
            
            var subscriptionId = await _subscriptionService.AddSubscriptionAsync(subscriptionDto);

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
