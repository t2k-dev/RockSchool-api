using RockSchool.BL.Services.TeacherService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers
{
    public class TeacherScreenDetailsService(ITeacherRepository teacherRepository) : ITeacherScreenDetailsService
    {
        public async Task<TeacherScreenDetailsResult> Query(Guid teacherId)
        {
            var teacher = await teacherRepository.GetByIdAsync(teacherId);

            //var attendanceInfos = new List<ParentAttendanceInfo>();
            //var subscriptionInfos = new List<ParentSubscriptionInfo>();

            /*var allAttendances = await attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(id, DateTime.MinValue, DateTime.MaxValue);
            if (allAttendances != null)
            {
                attendanceInfos = allAttendances.Where(a => a.GroupId == null).ToParentAttendanceInfos();
                var groupAttendanceInfos = AttendanceBuilder.BuildGroupAttendanceInfos(allAttendances.Where(a => a.GroupId != null));
                attendanceInfos.AddRange(groupAttendanceInfos);
            }

            var subscriptions = await subscriptionService.GetSubscriptionsByTeacherId(id);
            if (subscriptions != null)
            {
                subscriptionInfos = subscriptions.Where(a => a.GroupId == null).ToParentSubscriptionInfos();
                var groupSubscriptionInfos = SubscriptionBuilder.BuildGroupSubscriptionInfos(subscriptions.Where(a => a.GroupId != null));
                subscriptionInfos.AddRange(groupSubscriptionInfos);
            }*/

            var result = new TeacherScreenDetailsResult
            {
                Teacher = teacher,
                //Attendances = attendanceInfos.ToArray(),
                //Subscriptions = subscriptionInfos.ToArray(),
            };

            return result;
        }
    }
}
