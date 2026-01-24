using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TariffService;

public class TariffService(TariffRepository tariffRepository) : ITariffService
{
    public async Task<IEnumerable<Tariff>> GetAllTariffsAsync()
    {
        var tariffEntities = await tariffRepository.GetAllTariffsAsync();

        if (tariffEntities == null || tariffEntities.Length == 0)
            return [];

        return tariffEntities.ToModel();
    }

    public async Task<Tariff?> GetTrialTariffAsync()
    {
        var currentDate = DateTime.UtcNow;
        var tariffEntity = await tariffRepository.GetTrialTariffAsync(currentDate);
        return tariffEntity?.ToModel();
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
            return tariffEntities[0].ToModel();
        }

        if (disciplineId != null)
        {
            tariffEntities = tariffEntities.Where(t => t.DisciplineId == disciplineId).ToArray();
        }

        if (tariffEntities.Length != 1)
        {
            throw new InvalidOperationException("Unable to find tariffs");
        }

        return tariffEntities[0].ToModel();
    }

    public async Task<Tariff?> GetTariffAsync(Guid tariffId)
    {
        var tariff = await tariffRepository.GetByIdAsync(tariffId);
        return tariff?.ToModel();
    }

    public async Task<Guid> AddTariffAsync(Tariff tariff)
    {
        var tariffEntity = tariff.ToEntity();
        await tariffRepository.AddAsync(tariffEntity);
        return tariffEntity.TariffId;
    }
}