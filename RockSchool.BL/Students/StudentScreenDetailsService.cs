using RockSchool.BL.Bands;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Subscriptions;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Students;

public class StudentScreenDetailsService(
    IStudentService studentService,
    IAttendanceQueryService attendanceQueryService,
    ISubscriptionRepository subscriptionRepository,
    IBandMemberService bandMemberService) : IStudentScreenDetailsService
{
    public async Task<StudentScreenDetailsResult> Query(Guid studentId)
    {
        var student = await studentService.GetByIdAsync(studentId);

        var allAttendances = await attendanceQueryService.GetByStudentIdAsync(studentId);

        var subscriptions = await subscriptionRepository.GetSubscriptionsByStudentIdAsync(studentId);

        var bandMembers = await bandMemberService.GetByStudentIdAsync(studentId);
        var bands = bandMembers
            .Select(bm => bm.Band)
            .Where(b => b != null)
            .ToArray();

        var result = new StudentScreenDetailsResult
        {
            Student = student!,
            Attendances = allAttendances ?? [],
            Subscriptions = subscriptions ?? [],
            Bands = bands!
        };

        return result;
    }
}
