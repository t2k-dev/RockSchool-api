using RockSchool.BL.Services.AttendanceService;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class CancelSubscriptionService(
        IAttendanceService attendanceService,
        ISubscriptionRepository subscriptionRepository
        ) : ICancelSubscriptionService
    {
        public async Task Cancel(Guid subscriptionId, DateTime cancelDate, string cancelReason)
        {
            throw new NotImplementedException();
            // Update subscription
            /*var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            subscription.Cancel(cancelReason);

            await subscriptionRepository.UpdateSubscriptionAsync(subscription);


            // Cancel attendances
            await attendanceService.CancelFromDate(subscriptionId, cancelDate);*/
        }
    }
}
