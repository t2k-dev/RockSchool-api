using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.TariffService;

public class TariffService(ITariffRepository tariffRepository) : ITariffService
{
    public async Task<IEnumerable<Tariff>> GetAllTariffsAsync()
    {
        var tariffEntities = await tariffRepository.GetAllTariffsAsync();

        if (tariffEntities == null || tariffEntities.Length == 0)
            return [];

        return tariffEntities;
    }

    public async Task<Tariff?> GetTrialTariffAsync()
    {
        var currentDate = DateTime.UtcNow;
        var tariffEntity = await tariffRepository.GetTrialTariffAsync(currentDate);
        return tariffEntity;
    }

    public async Task<Tariff[]?> GetTariffsAsync(SubscriptionType subscriptionType)
    {
        var currentDate = DateTime.UtcNow;

        var tariffEntities = await tariffRepository.GetTariffsByTypeAsync(subscriptionType, currentDate);
        if (tariffEntities == null)
        {
            throw new InvalidOperationException("No tariffs found");
        }

        return tariffEntities;
    }

    public async Task<Tariff?> GetTariffAsync(SubscriptionType subscriptionType, int? disciplineId)
    {
        var currentDate = DateTime.UtcNow;

        var tariffEntities = await tariffRepository.GetTariffsByTypeAsync(subscriptionType, currentDate);
        if (tariffEntities == null)
        {
            throw new InvalidOperationException("No tariffs found");
        }
        
        if (tariffEntities.Length == 1)
        {
            return tariffEntities[0];
        }

        if (disciplineId != null)
        {
            tariffEntities = tariffEntities.Where(t => t.DisciplineId == disciplineId).ToArray();
        }

        if (tariffEntities.Length != 1)
        {
            throw new InvalidOperationException("Unable to find tariffs");
        }

        return tariffEntities[0];
    }

    public async Task<Tariff?> GetTariffAsync(Guid tariffId)
    {
        var tariff = await tariffRepository.GetByIdAsync(tariffId);
        return tariff;
    }

    public async Task<Guid> AddTariffAsync(Tariff tariff)
    {
        await tariffRepository.AddAsync(tariff);
        return tariff.TariffId;
    }
}