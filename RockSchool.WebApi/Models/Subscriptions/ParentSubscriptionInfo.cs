using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class ParentSubscriptionInfo : SubscriptionInfo
    {
        public SubscriptionInfo[] ChildSubscriptions { get; set; }
    }
}
