using System;
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Subscriptions;

public class AddSubscriptionRequest
{
    public Guid[] StudentIds { get; set; }
    public SubscriptionDetails Subscription { get; set; }
    public Schedule[] Schedules { get; set; }
}
