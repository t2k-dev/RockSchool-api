using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task AddAsync(User user);
    Task DeleteAsync(User? user);
    Task UpdateAsync(User user);
}
