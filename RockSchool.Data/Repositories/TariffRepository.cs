using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class TariffRepository : BaseRepository
{
    public TariffRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<TariffEntity[]> GetAllTariffsAsync()
    {
        return await RockSchoolContext.Tariffs
            .Include(t => t.Discipline)
            .ToArrayAsync();
    }

    public async Task<TariffEntity?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.Tariffs
            .Include(t => t.Discipline)
            .FirstOrDefaultAsync(t => t.TariffId == id);
    }

    public async Task AddAsync(TariffEntity tariffEntity)
    {
        await RockSchoolContext.Tariffs.AddAsync(tariffEntity);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TariffEntity tariffEntity)
    {
        RockSchoolContext.Tariffs.Update(tariffEntity);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TariffEntity tariffEntity)
    {
        RockSchoolContext.Tariffs.Remove(tariffEntity);
        await RockSchoolContext.SaveChangesAsync();
    }
}