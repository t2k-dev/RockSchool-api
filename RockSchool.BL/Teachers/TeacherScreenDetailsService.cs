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
    public class TeacherScreenDetailsService(ITeacherRepository teacherRepository, IAttendanceRepository attendanceRepository, IAttendanceQueryService attendanceQueryService) : ITeacherScreenDetailsService
    {
        public async Task<TeacherScreenDetailsResult> Query(Guid teacherId)
        {
            var teacher = await teacherRepository.GetByIdAsync(teacherId);

            //var attendanceInfos = new List<ParentAttendanceInfo>();
            //var subscriptionInfos = new List<ParentSubscriptionInfo>();

            var allAttendances = await attendanceQueryService.GetByTeacherIdForPeriodAsync(teacherId, DateTime.MinValue, DateTime.MaxValue);
            if (allAttendances != null)
            {
                
            }

            /*var subscriptions = await subscriptionService.GetSubscriptionsByTeacherId(id);
            if (subscriptions != null)
            {
                subscriptionInfos = subscriptions.Where(a => a.GroupId == null).ToParentSubscriptionInfos();
                var groupSubscriptionInfos = SubscriptionBuilder.BuildGroupSubscriptionInfos(subscriptions.Where(a => a.GroupId != null));
                subscriptionInfos.AddRange(groupSubscriptionInfos);
            }*/

            var result = new TeacherScreenDetailsResult
            {
                Teacher = teacher,
                Attendances = allAttendances.ToArray(),
                //Subscriptions = subscriptionInfos.ToArray(),
            };

            return result;
        }
    }
}
