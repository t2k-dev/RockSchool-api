using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task<int> AddStudentAsync(AddStudentServiceRequestDto addStudentServiceRequestDto);
    Task UpdateStudentAsync(UpdateStudentServiceRequestDto updateStudentServiceRequestDto);
    Task<StudentDto[]?> GetAllStudentsAsync();
    Task<StudentDto> GetById(int id);
    Task DeleteStudentAsync(int id);
}