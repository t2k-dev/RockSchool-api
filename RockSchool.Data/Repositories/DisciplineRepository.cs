using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Disciplines;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories;

public class DisciplineRepository : IDisciplineRepository
{
    private readonly RockSchoolContext _rockSchoolContext;

    public DisciplineRepository(RockSchoolContext rockSchoolContext)
    {
        _rockSchoolContext = rockSchoolContext;
    }

    public async Task<Discipline[]> GetAllAsync()
    {
        return await _rockSchoolContext.Disciplines.ToArrayAsync();
    }

    public async Task<Discipline?> GetByIdAsync(int id)
    {
        return await _rockSchoolContext.Disciplines
            //.Include(d => d.Teachers)
            .FirstOrDefaultAsync(d => d.DisciplineId == id);
    }

    public async Task<Discipline[]?> GetByIdsAsync(int[] ids)
    {
        return await _rockSchoolContext.Disciplines
                .Where(d => ids.Contains(d.DisciplineId))
                .ToArrayAsync();
    }

    public async Task AddAsync(Discipline discipline)
    {
        await _rockSchoolContext.Disciplines.AddAsync(discipline);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Discipline discipline)
    {
        _rockSchoolContext.Disciplines.Update(discipline);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Discipline discipline)
    {
        _rockSchoolContext.Disciplines.Remove(discipline);
        await _rockSchoolContext.SaveChangesAsync();
    }
}