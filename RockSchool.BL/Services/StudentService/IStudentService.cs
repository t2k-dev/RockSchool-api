using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task AddStudentAsync(AddStudentServiceRequestDto addStudentServiceRequestDto);
    Task UpdateStudentAsync(UpdateStudentServiceRequestDto updateStudentServiceRequestDto);
    Task<StudentDto[]?> GetAllStudentsAsync();
    Task DeleteStudentAsync(int id);
}