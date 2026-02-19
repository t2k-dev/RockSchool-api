using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IAttendeeRepository
{
    Task AddAsync(Attendee attendee);
}
