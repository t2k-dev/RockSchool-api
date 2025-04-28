using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public SubscriptionService(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
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
                TrialStatus = subscriptionDto.TrialStatus
            };

            await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
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

        public async Task<AttendanceDto> GetNextAvailableSlotAsync(Guid subscriptionId)
        {
            // TODO: complete after dev sync
            var subscription = _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
            return null;
        }

        public Task<AttendanceDto> RescheduleAttendance(Guid attendanceId, DateTime startDate)
        {
            //RescheduleAttendanceByStudent
            return null;
        }
    }
}
