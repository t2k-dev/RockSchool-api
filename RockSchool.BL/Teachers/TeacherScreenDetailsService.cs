using RockSchool.BL.Services.AttendanceService;
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
        IAttendanceQueryService attendanceQueryService,
        ISubscriptionRepository subscriptionRepository,
        IBandRepository bandRepository
        ) : ITeacherScreenDetailsService
    {
        public async Task<TeacherScreenDetailsResult> Query(Guid teacherId)
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(14);

            var teacher = await teacherRepository.GetByIdAsync(teacherId, startDate, endDate);

            var allAttendances = await attendanceQueryService.GetByTeacherIdForPeriodAsync(
                teacherId,
                startDate,
                endDate);

            var subscriptions = await subscriptionRepository.GetSubscriptionsByTeacherIdAsync(teacherId);

            var bands = await bandRepository.GetByTeacherIdAsync(teacherId);

            var result = new TeacherScreenDetailsResult
            {
                Teacher = teacher!,
                Attendances = allAttendances ?? [],
                Subscriptions = subscriptions ?? [],
                Bands = bands,
            };

            return result;
        }
    }
}
