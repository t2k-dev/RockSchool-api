using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IBandRepository
{
    Task<Band?> GetByIdAsync(Guid id);
    Task<Band[]> GetAllAsync();
    Task<Band[]> GetByTeacherIdAsync(Guid teacherId);
    Task<Guid> AddAsync(Band band);
    Task UpdateAsync(Band band);
    Task DeleteAsync(Guid id);
}
