using RockSchool.BL.Models;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ITrialSubscriptionService
    {
        Task CompleteTrial(Guid subscriptionId, TrialStatus trialStatus, string statusReason);
        Task<Guid> AddTrialSubscription(TrialRequestDto request);
    }
}
