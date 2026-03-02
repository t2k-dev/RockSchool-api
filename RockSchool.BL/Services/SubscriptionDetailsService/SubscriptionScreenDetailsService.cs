using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Students;
using RockSchool.BL.Subscriptions;
using RockSchool.BL.Teachers;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public class SubscriptionScreenDetailsService(
    ISubscriptionService subscriptionService,
    IScheduleService scheduleService,
    IStudentService studentService,
    ITeacherService teacherService,
    IAttendeeRepository attendeeRepository,
    IAttendanceRepository attendanceRepository,
    IPaymentRepository paymentRepository
    ) : ISubscriptionScreenDetailsService
{
    public async Task<SubscriptionScreenDetailsResult> Query(Guid subscriptionId)
    {
        var subscription = await subscriptionService.GetAsync(subscriptionId);
        if (subscription == null)
            throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

        // Get student
        var student = await studentService.GetByIdAsync(subscription.StudentId);
        if (student == null)
            throw new InvalidOperationException($"Student with id {subscription.StudentId} not found");

        // Get teacher if exists
        var teacher = subscription.TeacherId != null 
            ? await teacherService.GetTeacherByIdAsync(subscription.TeacherId.Value)
            : null;

        // Get schedules
        var schedules = await scheduleService.GetAllBySubscriptionIdAsync(subscriptionId);

        // Get attendances by collecting attendance IDs from attendees, then fetching them
        var attendees = await attendeeRepository.GetAllBySubscriptionIdAsync(subscriptionId);
        var attendanceIds = attendees.Select(a => a.AttendanceId).ToArray();
        
        var attendances = new List<Domain.Entities.Attendance>();
        foreach (var attendanceId in attendanceIds)
        {
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            if (attendance != null)
            {
                attendances.Add(attendance);
            }
        }

        // Get payments
        var payments = await paymentRepository.GetBySubscriptionIdAsync(subscriptionId);

        return new SubscriptionScreenDetailsResult
        {
            Subscription = subscription,
            Student = student,
            Teacher = teacher,
            Schedules = schedules ?? [],
            Attendances = attendances.ToArray(),
            Payments = payments ?? []
        };
    }
}
