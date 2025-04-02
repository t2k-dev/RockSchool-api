using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public SubscriptionService(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Guid> AddAsync(SubscriptionDto subscriptionDto)
        {
            var subscriptionEntity = new SubscriptionEntity
            {
                AttendanceCount = subscriptionDto.AttendanceCount,
                AttendanceLength = subscriptionDto.AttendanceLength,
                BranchId = subscriptionDto.BranchId,
                DisciplineId = subscriptionDto.DisciplineId,
                IsGroup = subscriptionDto.IsGroup,
                StartDate = subscriptionDto.StartDate,
                Status = subscriptionDto.Status,
                StudentId = subscriptionDto.StudentId,
                TeacherId = subscriptionDto.TeacherId,
                TransactionId = subscriptionDto.TransactionId,
                TrialStatus = subscriptionDto.TrialStatus
            };

            await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);

            return subscriptionEntity.SubscriptionId;
        }
    }
}
