using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Bands;

public class BandFormDataService(
    IBandRepository bandRepository,
    IScheduleSlotRepository scheduleSlotRepository) : IBandFormDataService
{
    public async Task<BandFormDataResult> Query(Guid bandId)
    {
        var band = await bandRepository.GetByIdAsync(bandId);
        if (band == null)
            throw new InvalidOperationException($"Band with id {bandId} not found");

        var scheduleSlots = band.ScheduleId != null
            ? await scheduleSlotRepository.GetByScheduleIdAsync(band.ScheduleId.Value)
            : [];

        return new BandFormDataResult
        {
            Band = band,
            ScheduleSlots = scheduleSlots ?? []
        };
    }
}
