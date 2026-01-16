using RockSchool.BL.Models;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task<RegisterStudentResponseDto> AddStudentAsync(Student studentDto, string email);
    Task UpdateStudentAsync(Student studentDto);
    Task<Student[]?> GetAllStudentsAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task DeleteStudentAsync(Guid id);
}