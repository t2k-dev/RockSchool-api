using RockSchool.BL.Services.AttendanceService;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Home
{
    public class HomeService(
        IBranchRepository branchRepository,
        IAttendanceRepository attendanceRepository,
        IAttendanceQueryService attendanceQueryService

        ) : IHomeService
    {
        public async Task<HomeDetailsDto> GetByBranch(int branchId)
        {
            var result = new HomeDetailsDto();

            result.Branch = await branchRepository.GetByIdAsync(branchId);

            result.Attendances = await attendanceRepository.GetByBranchIdAsync(branchId);

            return result;
        }

        public async Task<HomeDetailsWithAttendeesDto> GetByBranchWithAttendees(int branchId)
        {
            var branch = await branchRepository.GetByIdAsync(branchId);
            var attendances = await attendanceQueryService.GetByBranchIdAsync(branchId);

            return new HomeDetailsWithAttendeesDto
            {
                Branch = branch!,
                Attendances = attendances
            };
        }
    }
}
