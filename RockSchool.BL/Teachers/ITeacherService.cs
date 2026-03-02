using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers;

public interface ITeacherService
{
    Task<Teacher[]> GetAllTeachersAsync();
    Task<Teacher> GetTeacherByIdAsync(Guid id);
    Task UpdateTeacherAsync(TeacherDto teacherDto, bool updateDisciplines);
    Task UpdatePeriodsAsync(Guid teacherId, WorkingPeriodDto[] workingPeriodDtos, DateTime recalculatePeriodsAfter);
    Task SetTeacherActiveAsync(Guid id, bool isActive);
}