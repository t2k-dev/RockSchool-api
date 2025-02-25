using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task<Guid> AddStudentAsync(StudentDto studentDto);
    Task UpdateStudentAsync(StudentDto studentDto);
    Task<StudentDto[]?> GetAllStudentsAsync();
    Task<StudentDto?> GetByIdAsync(Guid id);
    Task DeleteStudentAsync(Guid id);
}