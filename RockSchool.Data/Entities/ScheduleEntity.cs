using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class ScheduleEntity
{
    [Key] 
    public int ScheduleId { get; set; }
    public int SubscriptionId { get; set; }
    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionEntity Subscription { get; set; }
    public int WeekDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}