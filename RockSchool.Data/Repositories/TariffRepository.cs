using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Tariffs;

namespace RockSchool.Data.Repositories;

public class TariffRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext), ITariffRepository
{
    public async Task<Tariff[]> GetAllTariffsAsync()
    {
        return await RockSchoolContext.Tariffs
            .Include(t => t.Discipline)
            .ToArrayAsync();
    }

    public async Task<Tariff[]?> GetTariffsByTypeAsync(SubscriptionType type, DateTime date)
    {
        return await RockSchoolContext.Tariffs
            .Where(t => t.SubscriptionType == type
                                      && t.StartDate <= date
                                      && t.EndDate >= date)
            .ToArrayAsync();
    }

    public async Task<Tariff?> GetTrialTariffAsync(DateTime date)
    {
        return await RockSchoolContext.Tariffs
            .FirstOrDefaultAsync(t => t.SubscriptionType == SubscriptionType.TrialLesson 
                                   && t.StartDate <= date 
                                   && t.EndDate >= date);
    }

    public async Task<Tariff?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.Tariffs
            .Include(t => t.Discipline)
            .FirstOrDefaultAsync(t => t.TariffId == id);
    }

    public async Task AddAsync(Tariff tariff)
    {
        await RockSchoolContext.Tariffs.AddAsync(tariff);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tariff tariff)
    {
        RockSchoolContext.Tariffs.Update(tariff);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tariff tariff)
    {
        RockSchoolContext.Tariffs.Remove(tariff);
        await RockSchoolContext.SaveChangesAsync();
    }
}