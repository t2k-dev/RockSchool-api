using RockSchool.BL.Models;

namespace RockSchool.BL.Services.TeacherService;

public interface ITeacherService
{
    Task<RegisterTeacherResponseDto> AddTeacher(Teacher addTeacherDto, string email);
    Task<Teacher[]> GetAllTeachersAsync();
    Task<Teacher> GetTeacherByIdAsync(Guid id);
    Task UpdateTeacherAsync(Teacher teacherDto, bool updateDisciplines, bool recalculatePeriods);
    Task SetTeacherActiveAsync(Guid id, bool isActive);
    Task DeleteTeacherAsync(Guid id);
    Task<Teacher[]?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
}