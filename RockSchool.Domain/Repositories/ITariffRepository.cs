using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Repositories;

public interface ITariffRepository
{
    Task<Tariff[]> GetAllTariffsAsync();
    Task<Tariff[]?> GetTariffsByTypeAsync(SubscriptionType type, DateTime date);
    Task<Tariff?> GetTrialTariffAsync(DateTime date);
    Task<Tariff?> GetByIdAsync(Guid id);
    Task AddAsync(Tariff tariff);
    Task UpdateAsync(Tariff tariff);
    Task DeleteAsync(Tariff tariff);
}
