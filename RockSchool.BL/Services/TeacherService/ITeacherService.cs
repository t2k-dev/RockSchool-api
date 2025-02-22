using RockSchool.BL.Dtos.Service.Requests.TeacherService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.TeacherService;

public interface ITeacherService
{
    Task AddTeacher(TeacherDto addTeacherDto);
    Task<TeacherDto[]> GetAllTeachersAsync();
    Task<TeacherDto> GetTeacherByIdAsync(Guid id);
    Task UpdateTeacherAsync(UpdateTeacherServiceRequestDto updateTeacherServiceRequestDto);
    Task DeleteTeacherAsync(Guid id);
}