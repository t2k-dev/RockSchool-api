using RockSchool.Domain.Entities;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Subscriptions;

public class SubscriptionScreenDetailsResult
{
    public Subscription Subscription { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public Teacher? Teacher { get; set; }
    public Schedule[] Schedules { get; set; } = [];
    public Attendance[] Attendances { get; set; } = [];
    public Payment[] Payments { get; set; } = [];
}
