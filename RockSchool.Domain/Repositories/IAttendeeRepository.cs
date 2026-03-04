using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IAttendeeRepository
{
    Task AddAsync(Attendee attendee);
    Task<Attendee?> GetAsync(Guid attendeeId);
    Task<Attendee[]> GetAllBySubscriptionIdAsync(Guid subscriptionId);
    void Update(Attendee attendee);
}
