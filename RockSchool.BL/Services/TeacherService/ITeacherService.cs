using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.TeacherService;

public interface ITeacherService
{
    Task AddTeacher(TeacherDto addTeacherDto);
    Task<TeacherDto[]> GetAllTeachersAsync();
    Task<TeacherDto> GetTeacherByIdAsync(Guid id);
    Task UpdateTeacherAsync(TeacherDto teacherDto);
    Task DeleteTeacherAsync(Guid id);
}