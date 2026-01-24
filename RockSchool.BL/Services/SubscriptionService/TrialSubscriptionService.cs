using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.TariffService;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class TrialSubscriptionService(
        SubscriptionRepository subscriptionRepository,
        IAttendanceService attendanceService,
        INoteService noteService,
        ISubscriptionService subscriptionService,
        ITariffService tariffService
        ) : ITrialSubscriptionService
    {
        public async Task CompleteTrial(Guid subscriptionId, TrialStatus trialStatus, string statusReason)
        {
            var subscriptionEntity = await subscriptionRepository.GetAsync(subscriptionId);

            subscriptionEntity.AttendancesLeft -= 1;
            subscriptionEntity.Status = SubscriptionStatus.Completed;
            subscriptionEntity.TrialStatus = trialStatus;
            subscriptionEntity.StatusReason = statusReason;

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);
        }

        public async Task<Guid> AddTrialSubscription(TrialRequestDto request)
        {
            var tariff = await tariffService.GetTariffAsync(request.TariffId);
            if (tariff == null)
            {
                throw new InvalidOperationException("Tariff is not found");
            }

            var subscription = new Subscription
            {
                SubscriptionType = SubscriptionType.TrialLesson,
                Status = SubscriptionStatus.Draft,
                TrialStatus = TrialStatus.Created,

                DisciplineId = request.DisciplineId,
                StudentId = request.Student.StudentId,
                TeacherId = request.TeacherId,
                AttendancesLeft = 1,
                BranchId = request.BranchId,
                GroupId = null,
                StartDate = DateOnly.FromDateTime(request.TrialDate),

                TariffId = request.TariffId,
                AttendanceCount = tariff.AttendanceCount,
                AttendanceLength = tariff.AttendanceLength,
                Price = tariff.Amount,
                AmountOutstanding = tariff.Amount,
                FinalPrice = tariff.Amount,
            };
            
            var subscriptionId = await subscriptionService.AddSubscriptionAsync(subscription);

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
                AttendanceType = AttendanceType.TrialLesson,
            };

            await attendanceService.AddAttendanceAsync(trialAttendance);

            await noteService.AddNoteAsync(request.BranchId, $"Пробное занятие {request.Student.FirstName}.", request.TrialDate.ToUniversalTime());

            return subscriptionId;
        }
    }
}
