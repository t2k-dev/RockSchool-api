using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Teachers.AvailableTeachers
{
    public class AvailableTeachersService(
        ITeacherRepository teacherRepository,
        IAttendanceRepository attendanceRepository
        ) : IAvailableTeachersService
    {
        public async Task<AvailableTeachersDto[]> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
        {
            var loadDate = DateTime.UtcNow.AddDays(-7);

            var teachers = await teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge, loadDate);
            var result = new List<AvailableTeachersDto>();
            foreach (var teacher in teachers)
            {
                var allAttendances = await attendanceRepository.GetByTeacherIdForPeriodOfTimeAsync(
                    teacher.TeacherId,
                    DateTime.UtcNow.AddDays(-7),
                    DateTime.MaxValue);

                result.Add(new AvailableTeachersDto
                {
                    Teacher = teacher,
                    Attendances = allAttendances,
                    Workload = Random.Shared.Next(1, 100),
                });
            }

            return result.ToArray();
        }

    }
}
