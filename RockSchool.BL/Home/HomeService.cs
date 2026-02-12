using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Home
{
    public class HomeService(
        IBranchRepository branchRepository
        ) : IHomeService
    {
        public async Task<HomeDetailsDto> GetByBranch(int branchId)
        {
            var result = new HomeDetailsDto();

            result.Branch = await branchRepository.GetByIdAsync(branchId);

            //var notes = await noteService.GetNotesAsync(branchId);

            //var allAttendances = await attendanceService.GetByBranchIdAsync(branchId);
            /*
            var attendanceInfos = allAttendances.Where(a => a.GroupId == null).ToParentAttendanceInfos();
            var groupAttendanceInfos = AttendanceBuilder.BuildGroupAttendanceInfos(allAttendances.Where(a => a.GroupId != null));
            attendanceInfos.AddRange(groupAttendanceInfos);
            */
            return result;
        }
    }
}
