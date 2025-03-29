using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.AvailableTeachersService
{
    public class AvailableTeachersService : IAvailableTeachersService
    {
        private readonly TeacherRepository _teacherRepository;
        private readonly AttendanceRepository _attendanceRepository;

        public AvailableTeachersService(TeacherRepository teacherRepository, AttendanceRepository attendanceRepository)
        {
            _teacherRepository = teacherRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AvailableTeachersDto?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
        {

            throw new NotImplementedException();
            // var teacherEntities = await _teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge);
            //
            // if (teacherEntities.Length == 0)
            //     return null;
            //
            // var allAttendanceDtos = new List<AvailabilityAttendanceDto>();
            //
            // foreach (var teacherEntity in teacherEntities)
            // {
            //     var attendances = await _attendanceRepository.GetAttendancesByTeacherIdForPeriodOfTimeAsync(
            //         teacherEntity.TeacherId,
            //         DateTime.UtcNow.AddMonths(-1),
            //         DateTime.UtcNow.AddMonths(1),
            //         AttendanceStatus.New
            //     );
            //
            //     if (attendances != null)
            //     {
            //         var mapped = attendances.Select(a => new AvailabilityAttendanceDto
            //         {
            //             TeacherId = teacherEntity.TeacherId,
            //             StartDate = a.StartDate,
            //             EndDate = a.EndDate,
            //             Status = (int)a.Status
            //         });
            //
            //         allAttendanceDtos.AddRange(mapped);
            //     }
            // }
            //
            // var teachers = CreateAvailableTeachersArray(teacherEntities);
            // var scheduledPeriods = CreateScheduledWorkingPeriodsArray(teacherEntities);
            //
            // return new AvailableTeachersDto
            // {
            //     AvailableTeachers = teachers,
            //     ScheduledWorkingPeriods = scheduledPeriods,
            //     Attendancies = allAttendanceDtos.ToArray()
            // };
        }


        private AvailableTeacherDto[] CreateAvailableTeachersArray(TeacherEntity[] teacherEntities)
        {
            return teacherEntities
                .Select(t => new AvailableTeacherDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    WorkLoad = Random.Shared.Next(0, 101)
                })
                .ToArray();
        }

        private ScheduledWorkingPeriodDto[] CreateScheduledWorkingPeriodsArray(TeacherEntity[] teacherEntities)
        {
            var result = new List<ScheduledWorkingPeriodDto>();

            foreach (var teacher in teacherEntities)
            {
                if (teacher.WorkingPeriods == null)
                    continue;

                foreach (var workingPeriod in teacher.WorkingPeriods)
                {
                    if (workingPeriod.ScheduledWorkingPeriods == null)
                        continue;

                    foreach (var swp in workingPeriod.ScheduledWorkingPeriods)
                    {
                        result.Add(new ScheduledWorkingPeriodDto
                        {
                            TeacherId = teacher.TeacherId,
                            ScheduledWorkingPeriodId = swp.ScheduledWorkingPeriodId,
                            StartDate = swp.StartDate,
                            EndDate = swp.EndDate
                        });
                    }
                }
            }

            return result.ToArray();
        }
    }
}