using RockSchool.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<Guid> AddAsync(SubscriptionDto subscriptionDto);
    }
}
