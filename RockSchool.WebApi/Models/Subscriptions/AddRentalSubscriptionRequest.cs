using System;
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class AddRentalSubscriptionRequest
    {
        public SubscriptionDetails SubscriptionDetails { get; set; }
        public Schedule[] Schedules { get; set; }
    }
}
