using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription);
    Task<Subscription?> GetAsync(Guid id);
    Task<Subscription[]> GetAllSubscriptionsAsync();
    Task<Subscription[]?> GetSubscriptionsByStudentIdAsync(Guid studentId);
    Task<Subscription[]?> GetSubscriptionsByTeacherIdAsync(Guid teacherId);
    Task<Subscription[]?> GetByGroupIdAsync(Guid groupId);
    void Update(Subscription subscription);
    Task DeleteSubscriptionAsync(Subscription subscription);
}
