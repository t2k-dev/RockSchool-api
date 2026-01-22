using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TariffService;

public class TariffService : ITariffService
{
    private readonly TariffRepository _tariffRepository;

    public TariffService(TariffRepository tariffRepository)
    {
        _tariffRepository = tariffRepository;
    }

    public async Task<IEnumerable<Tariff>> GetAllTariffsAsync()
    {
        var tariffEntities = await _tariffRepository.GetAllTariffsAsync();

        if (tariffEntities == null || tariffEntities.Length == 0)
            return [];

        return tariffEntities.ToModel();
    }

    public async Task<Guid> AddTariffAsync(Tariff tariff)
    {
        var tariffEntity = tariff.ToEntity();
        await _tariffRepository.AddAsync(tariffEntity);
        return tariffEntity.TariffId;
    }
}