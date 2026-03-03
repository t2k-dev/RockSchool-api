using RockSchool.Domain.Entities;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Subscriptions;

public class SubscriptionFormDataResult
{
    public Subscription Subscription { get; set; } = null!;
    public Student[] Students { get; set; } = [];
    public Teacher? Teacher { get; set; }
    public ScheduleSlot[] ScheduleSlots { get; set; } = [];
    public Payment[] Payments { get; set; } = [];
}
