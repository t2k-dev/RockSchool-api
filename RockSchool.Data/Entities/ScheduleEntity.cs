using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class ScheduleEntity
{
    [Key] 
    public Guid ScheduleId { get; set; }

    public Guid? SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionEntity? Subscription { get; set; }

    public Guid? BandId { get; set; }

    [ForeignKey(nameof(BandId))]
    public BandEntity? Band { get; set; }

    public int RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public RoomEntity? Room { get; set; }

    public int WeekDay { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}