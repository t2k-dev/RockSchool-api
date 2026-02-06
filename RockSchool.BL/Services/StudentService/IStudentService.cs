using RockSchool.Domain.Students;

namespace RockSchool.BL.Services.StudentService;

public interface IStudentService
{
    Task<Guid> AddStudentAsync(Student studentDto);
    Task UpdateStudentAsync(Student studentDto);
    Task<Student[]?> GetAllStudentsAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task DeleteStudentAsync(Guid id);
}