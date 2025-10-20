using System;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Subscriptions;

public class AddSubscriptionRequest
{
    public Guid[] StudentIds { get; set; }
    public SubscriptionDetails Subscription { get; set; }
    public ScheduleDto[] Schedules { get; set; }
}
