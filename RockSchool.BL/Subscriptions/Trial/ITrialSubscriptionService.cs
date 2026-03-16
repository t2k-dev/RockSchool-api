using RockSchool.BL.Models;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Subscriptions.Trial
{
    public interface ITrialSubscriptionService
    {
        Task AddTrial(AddTrialDto addTrialDto);

        Task AcceptTrial(Guid attendanceId, Guid subscriptionId, string statusReason, string comment);

        Task DeclineTrial(Guid attendanceId, Guid subscriptionId, string statusReason, string comment);
    }
}
