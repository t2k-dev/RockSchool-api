using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetAsync(Guid id);
    Task<Subscription[]> GetAllSubscriptionsAsync();
    Task<Subscription[]?> GetSubscriptionsByStudentIdAsync(Guid studentId);
    Task<Subscription[]?> GetSubscriptionsByTeacherIdAsync(Guid teacherId);
    Task<Subscription[]?> GetByGroupIdAsync(Guid groupId);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(Subscription subscription);
}
