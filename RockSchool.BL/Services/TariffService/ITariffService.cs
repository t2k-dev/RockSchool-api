using RockSchool.BL.Models;

namespace RockSchool.BL.Services.TariffService;

public interface ITariffService
{
    Task<IEnumerable<Tariff>> GetAllTariffsAsync();
    Task<Guid> AddTariffAsync(Tariff tariff);
}