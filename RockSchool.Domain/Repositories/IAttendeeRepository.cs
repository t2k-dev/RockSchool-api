using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IAttendeeRepository
{
    void AddAsync(Attendee attendee);
}
