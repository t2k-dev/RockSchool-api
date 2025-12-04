using RockSchool.BL.Models;
using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }
    public Guid SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    public int WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int RoomId { get; set; }
}