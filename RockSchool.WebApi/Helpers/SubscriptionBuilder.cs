using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Subscriptions;
using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Models;

namespace RockSchool.WebApi.Helpers
{
    public class SubscriptionBuilder
    {
        public static List<ParentSubscriptionInfo> BuildGroupSubscriptionInfos(IEnumerable<Subscription> subscriptions)
        {
            var parentSubscriptions = new List<ParentSubscriptionInfo>();

            var groupIds = subscriptions.Select(s => s.GroupId).Distinct();

            foreach (var groupId in groupIds)
            {
                var currentGroupSubscriptions = subscriptions.Where(a => a.GroupId == groupId);
                var parentSubscription = currentGroupSubscriptions.First().ToParentSubscriptionInfo();
                parentSubscription.ChildSubscriptions = currentGroupSubscriptions.ToSubscriptionInfos().ToArray();

                parentSubscriptions.Add(parentSubscription);
            }

            return parentSubscriptions;
        }
    }
}
