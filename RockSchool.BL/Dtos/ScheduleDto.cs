using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }
    public Guid SubscriptionId { get; set; }
    public SubscriptionDto? Subscription { get; set; }
    public int WeekDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int RoomId { get; set; }

}