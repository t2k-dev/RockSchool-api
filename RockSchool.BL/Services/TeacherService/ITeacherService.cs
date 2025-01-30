using RockSchool.BL.Dtos.Service.Requests.TeacherService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.TeacherService;

public interface ITeacherService
{
    Task AddTeacher(AddTeacherServiceRequestDto addTeacherDto);
    Task<TeacherDto[]> GetAllTeachersAsync();
    Task<TeacherDto> GetTeacherByIdAsync(int id);
    Task UpdateTeacherAsync(UpdateTeacherServiceRequestDto updateTeacherServiceRequestDto);
    Task DeleteTeacherAsync(int id);
}
