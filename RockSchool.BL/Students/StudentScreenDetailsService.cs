using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BandStudentService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;

namespace RockSchool.BL.Students;

public class StudentScreenDetailsService(
    IStudentService studentService,
    IAttendanceQueryService attendanceQueryService,
    ISubscriptionService subscriptionService,
    IBandStudentService bandStudentService) : IStudentScreenDetailsService
{
    public async Task<StudentScreenDetailsResult> Query(Guid studentId)
    {
        var student = await studentService.GetByIdAsync(studentId);

        var allAttendances = await attendanceQueryService.GetByStudentIdAsync(studentId);

        var subscriptions = await subscriptionService.GetSubscriptionsByStudentId(studentId);

        var bandStudents = await bandStudentService.GetByStudentIdAsync(studentId);
        var bands = bandStudents
            .Select(bs => bs.Band)
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
