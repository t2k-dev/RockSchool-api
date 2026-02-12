using RockSchool.BL.Models;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Subscriptions.Trial
{
    public interface ITrialSubscriptionService
    {
        Task AddTrial(AddTrialDto addTrialDto);
        Task CompleteTrial(Guid subscriptionId, TrialStatus trialStatus, string statusReason);
    }
}
