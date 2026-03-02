using System;
using RockSchool.BL.Subscriptions;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class AddRentalSubscriptionRequest
    {
        public SubscriptionDetails SubscriptionDetails { get; set; }
        public ScheduleInfo[] Schedules { get; set; }
    }
}
