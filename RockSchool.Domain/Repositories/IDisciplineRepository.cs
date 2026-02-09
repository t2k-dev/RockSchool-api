using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IDisciplineRepository
{
    Task<Discipline[]> GetAllAsync();
    Task<Discipline?> GetByIdAsync(int id);
    Task<Discipline[]?> GetByIdsAsync(int[] ids);
    Task AddAsync(Discipline discipline);
    Task UpdateAsync(Discipline discipline);
    Task DeleteAsync(Discipline discipline);
}
