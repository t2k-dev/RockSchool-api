using RockSchool.BL.Models;

namespace RockSchool.BL.Services.BusySlotsService;

public interface IBusySlotsService
{
    Task<BusySlotsResultDto[]> GetBusySlotsByBranchAsync(int branchId);
}
