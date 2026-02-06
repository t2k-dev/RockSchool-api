using RockSchool.BL.Teachers;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Services.TeacherService;

public interface ITeacherService
{
    Task<Guid> AddTeacher(Teacher addTeacherDto);
    Task<Teacher[]> GetAllTeachersAsync();
    Task<Teacher> GetTeacherByIdAsync(Guid id);
    Task UpdateTeacherAsync(TeacherDto teacherDto, bool updateDisciplines, bool recalculatePeriods);
    Task SetTeacherActiveAsync(Guid id, bool isActive);
    Task DeleteTeacherAsync(Guid id);
    Task<Teacher[]?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
    Task<Teacher[]?> GetRehearsableTeachersAsync(int branchId);
}