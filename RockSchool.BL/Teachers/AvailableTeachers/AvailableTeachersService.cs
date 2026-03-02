using RockSchool.Domain.Repositories;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers.AvailableTeachers
{
    public class AvailableTeachersService(
        ITeacherRepository teacherRepository,
        IAttendanceRepository attendanceRepository
        ) : IAvailableTeachersService
    {
        public async Task<AvailableTeachersDto[]> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
        {
            var result = new List<AvailableTeachersDto>();

            var availableTeachers = await teacherRepository.GetAvailableTeachersAsync(branchId, disciplineId, studentAge);
            foreach (var availableTeacher in availableTeachers)
            {
                var availableTeachersDto = await GetTeacherWithAttendances(availableTeacher.TeacherId);
                result.Add(availableTeachersDto);
            }

            return result.ToArray();
        }

        public async Task<AvailableTeachersDto[]?> GetRehearsableTeachersAsync(int branchId)
        {
            var result = new List<AvailableTeachersDto>();

            var availableTeachers = await teacherRepository.GetRehearsableTeachersAsync(branchId);
            foreach (var availableTeacher in availableTeachers)
            {
                var availableTeachersDto = await GetTeacherWithAttendances(availableTeacher.TeacherId);
                result.Add(availableTeachersDto);
            }

            return result.ToArray();
        }

        public async Task<AvailableTeachersDto> GetAvailableTeacherAsync(Guid teacherId)
        {
            return await GetTeacherWithAttendances(teacherId);
        }

        private async Task<AvailableTeachersDto> GetTeacherWithAttendances(Guid teacherId)
        {
            var loadDate = DateTime.UtcNow.AddDays(-7);

            var teacher = await teacherRepository.GetTeacherAsync(teacherId, loadDate);

            var allAttendances = await attendanceRepository.GetByTeacherIdForPeriodOfTimeAsync(
                teacher.TeacherId,
                DateTime.UtcNow.AddDays(-7),
                DateTime.MaxValue);

            return new AvailableTeachersDto
            {
                Teacher = teacher,
                Attendances = allAttendances,
                Workload = Random.Shared.Next(1, 100),
            };
        }
    }
}
