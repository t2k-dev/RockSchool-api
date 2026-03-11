using RockSchool.BL.Services.AttendanceService;

namespace RockSchool.BL.Bands;

public class BandScreenDetailsService(
    IBandService bandService,
    IAttendanceQueryService attendanceQueryService,
    IBandMemberService bandMemberService) : IBandScreenDetailsService
{
    public async Task<BandScreenDetailsResult> Query(Guid bandId)
    {
        var band = await bandService.GetByIdAsync(bandId);

        var allAttendances = await attendanceQueryService.GetByBandIdAsync(bandId);

        var bandMembers = await bandMemberService.GetByBandIdAsync(bandId);

        var result = new BandScreenDetailsResult
        {
            Band = band!,
            Attendances = allAttendances ?? [],
            BandMembers = bandMembers
        };

        return result;
    }
}
