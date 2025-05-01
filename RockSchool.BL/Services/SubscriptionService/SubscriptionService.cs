using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly ScheduleRepository _scheduleRepository;
        private readonly AttendanceRepository _attendanceRepository;

        public SubscriptionService(SubscriptionRepository subscriptionRepository, ScheduleRepository scheduleRepository, AttendanceRepository attendanceRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _scheduleRepository = scheduleRepository;
            _attendanceRepository = attendanceRepository;
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
            var orderedSchedules = schedules.OrderBy(s => s.WeekDay).ToArray();
            
            var nextDate = ScheduleHelper.GetNextAttendanceDate(lastAttendance.StartDate, orderedSchedules);
            
            return nextDate;
        }

        public Task<AttendanceDto> RescheduleAttendance(Guid attendanceId, DateTime startDate)
        {
            //RescheduleAttendanceByStudent
            return null;
        }
    }
}
