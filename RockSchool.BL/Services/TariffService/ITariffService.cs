using RockSchool.BL.Models;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.TariffService;

public interface ITariffService
{
    Task<IEnumerable<Tariff>> GetAllTariffsAsync();
    Task<Tariff?> GetTrialTariffAsync();
    Task<Tariff?> GetTariffAsync(SubscriptionType subscriptionType, int? disciplineId);
    Task<Tariff?> GetTariffAsync(Guid tariffId);
    Task<Guid> AddTariffAsync(Tariff tariff);
}