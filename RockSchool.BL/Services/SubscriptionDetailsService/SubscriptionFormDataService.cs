using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Students;
using RockSchool.BL.Subscriptions;
using RockSchool.BL.Teachers;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public class SubscriptionFormDataService(
    ISubscriptionService subscriptionService,
    IStudentService studentService,
    ITeacherService teacherService,
    IScheduleSlotRepository scheduleSlotRepository,
    IPaymentRepository paymentRepository
    ) : ISubscriptionFormDataService
{
    public async Task<SubscriptionFormDataResult> Query(Guid subscriptionId)
    {
        var subscription = await subscriptionService.GetAsync(subscriptionId);
        if (subscription == null)
            throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

        // Get students (handle group subscriptions)
        var students = new List<Domain.Students.Student>();
        if (subscription.GroupId != null)
        {
            var groupSubscriptions = await subscriptionService.GetSubscriptionByGroupIdAsync(subscription.GroupId.Value);
            if (groupSubscriptions != null)
            {
                foreach (var groupSub in groupSubscriptions)
                {
                    var student = await studentService.GetByIdAsync(groupSub.StudentId);
                    if (student != null)
                        students.Add(student);
                }
            }
        }
        else
        {
            var student = await studentService.GetByIdAsync(subscription.StudentId);
            if (student != null)
                students.Add(student);
        }

        // Get teacher if exists
        var teacher = subscription.TeacherId != null 
            ? await teacherService.GetTeacherByIdAsync(subscription.TeacherId.Value)
            : null;

        // Get schedule slots for this subscription's schedule
        var scheduleSlots = subscription.ScheduleId != null
            ? await scheduleSlotRepository.GetByScheduleIdAsync(subscription.ScheduleId.Value)
            : [];

        // Get payments
        var payments = await paymentRepository.GetBySubscriptionIdAsync(subscriptionId);

        return new SubscriptionFormDataResult
        {
            Subscription = subscription,
            Students = students.ToArray(),
            Teacher = teacher,
            ScheduleSlots = scheduleSlots ?? [],
            Payments = payments ?? []
        };
    }
}
