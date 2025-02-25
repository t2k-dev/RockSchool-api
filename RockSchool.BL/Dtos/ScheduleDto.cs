using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }
    public SubscriptionEntity Subscription { get; set; }
    public int WeekDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}