using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task<Guid> AddStudentAsync(AddStudentServiceRequestDto addStudentServiceRequestDto);
    Task UpdateStudentAsync(UpdateStudentServiceRequestDto updateStudentServiceRequestDto);
    Task<StudentDto[]?> GetAllStudentsAsync();
    Task<StudentDto?> GetByIdAsync(Guid id);
    Task DeleteStudentAsync(Guid id);
}