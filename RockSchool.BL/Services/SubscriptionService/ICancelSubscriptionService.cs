using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ICancelSubscriptionService
    {
        public Task Cancel(Guid subscriptionId, DateTime cancelDate, string cancelReason);
    }
}
