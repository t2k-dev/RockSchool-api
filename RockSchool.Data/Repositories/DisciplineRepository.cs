using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class DisciplineRepository
{
    private readonly RockSchoolContext _rockSchoolContext;

    public DisciplineRepository(RockSchoolContext rockSchoolContext)
    {
        _rockSchoolContext = rockSchoolContext;
    }

    public async Task<DisciplineEntity[]> GetAllAsync()
    {
        return await _rockSchoolContext.Disciplines.ToArrayAsync();
    }

    public async Task<DisciplineEntity?> GetByIdAsync(int id)
    {
        return await _rockSchoolContext.Disciplines
            .Include(d => d.Teachers)
            .FirstOrDefaultAsync(d => d.DisciplineId == id);
    }

    public async Task AddAsync(DisciplineEntity disciplineEntity)
    {
        await _rockSchoolContext.Disciplines.AddAsync(disciplineEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(DisciplineEntity disciplineEntity)
    {
        _rockSchoolContext.Disciplines.Update(disciplineEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(DisciplineEntity disciplineEntity)
    {
        _rockSchoolContext.Disciplines.Remove(disciplineEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }
}