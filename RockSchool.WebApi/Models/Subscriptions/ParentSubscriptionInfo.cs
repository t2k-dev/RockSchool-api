using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class ParentSubscriptionInfo : SubscriptionReachInfo
    {
        public SubscriptionReachInfo[] ChildSubscriptions { get; set; }
    }
}
