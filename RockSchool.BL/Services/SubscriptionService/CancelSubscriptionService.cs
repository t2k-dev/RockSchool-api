using RockSchool.BL.Services.AttendanceService;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class CancelSubscriptionService(
        IAttendanceService attendanceService,
        SubscriptionRepository subscriptionRepository
        ) : ICancelSubscriptionService
    {
        public async Task Cancel(Guid subscriptionId, DateTime cancelDate, string cancelReason)
        {
            // Update subscription
            var subscriptionEntity = await subscriptionRepository.GetAsync(subscriptionId);

            subscriptionEntity.StatusReason = cancelReason;
            subscriptionEntity.Status = SubscriptionStatus.Cancelled;

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);


            // Cancel attendances
            await attendanceService.CancelFromDate(subscriptionId, cancelDate);
        }
    }
}
