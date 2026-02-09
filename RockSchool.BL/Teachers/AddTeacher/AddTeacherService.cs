using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers.AddTeacher
{
    public class AddTeacherService(ITeacherRepository teacherRepository, IDisciplineRepository disciplineRepository): IAddTeacherService
    {
        public async Task<Guid> Handle(TeacherDto request)
        {
            var teacher = Teacher.Create(
                request.FirstName, 
                request.LastName, 
                request.BirthDate, 
                request.Sex,
                request.Phone, 
                request.BranchId, 
                request.AgeLimit,
                request.AllowGroupLessons,
                request.AllowBands
                );

            var disciplines = await disciplineRepository.GetByIdsAsync(request.DisciplineIds);
            teacher.UpdateDisciplines(disciplines);

            await teacherRepository.AddAsync(teacher);

            var workingPeriodEntities = request.WorkingPeriods;
            //var scheduledWorkingPeriods = BuildScheduledWorkingPeriods(workingPeriodEntities., teacher.TeacherId, DateTime.Now.ToUniversalTime(), 3);

            return teacher.TeacherId;
        }

        private List<ScheduledWorkingPeriod> BuildScheduledWorkingPeriods(WorkingPeriod[] workingPeriods, Guid teacherId, DateTime startDate, int months)
        {
            var scheduledWorkingPeriodEntities = new List<ScheduledWorkingPeriod>();

            startDate = startDate.Date;
            var endDate = startDate.AddMonths(months);

            for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                foreach (var workingPeriodEntity in workingPeriods)
                {
                    if ((int)currentDate.DayOfWeek == workingPeriodEntity.WeekDay)
                    {
                        var localStart = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.StartTime), DateTimeKind.Local);
                        var localEnd = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.EndTime), DateTimeKind.Local);

                        var periodStart = localStart.ToUniversalTime();
                        var periodEnd = localEnd.ToUniversalTime();

                        var scheduledWorkingPeriod = ScheduledWorkingPeriod.Create(teacherId, periodStart, periodEnd, workingPeriodEntity.RoomId);
                        scheduledWorkingPeriodEntities.Add(scheduledWorkingPeriod);
                    }
                }
            }

            return scheduledWorkingPeriodEntities;
        }
    }
}
