using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Teachers
{
    public class TeacherScreenDetailsService(
        ITeacherRepository teacherRepository,
        IAttendanceRepository attendanceRepository,
        IAttendanceQueryService attendanceQueryService,
        ISubscriptionRepository subscriptionRepository
        ) : ITeacherScreenDetailsService
    {
        public async Task<TeacherScreenDetailsResult> Query(Guid teacherId)
        {
            var teacher = await teacherRepository.GetByIdAsync(teacherId);

            //var subscriptionInfos = new List<ParentSubscriptionInfo>();

            var allAttendances = await attendanceQueryService.GetByTeacherIdForPeriodAsync(teacherId, DateTime.MinValue, DateTime.MaxValue);
            if (allAttendances != null)
            {
                
            }

            var subscriptions = await subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);
            if (subscriptions != null)
            {

            }

            var result = new TeacherScreenDetailsResult
            {
                Teacher = teacher,
                Attendances = allAttendances.ToArray(),
                Subscriptions = subscriptions.ToArray(),
            };

            return result;
        }
    }
}
