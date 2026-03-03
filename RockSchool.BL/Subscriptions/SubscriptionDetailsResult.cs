using RockSchool.Domain.Entities;

namespace RockSchool.BL.Subscriptions;

public class SubscriptionDetailsResult
{
    public Subscription Subscription { get; set; } = null!;
    public ScheduleSlot[] ScheduleSlots { get; set; } = [];
    public Payment[] Payments { get; set; } = [];
}
