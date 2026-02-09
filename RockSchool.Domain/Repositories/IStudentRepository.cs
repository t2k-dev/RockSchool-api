using RockSchool.Domain.Students;

namespace RockSchool.Domain.Repositories;

public interface IStudentRepository
{
    Task AddAsync(Student student);
    Task<Student?> GetByIdAsync(Guid studentId);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
    Task<Student[]> GetAllAsync();
}
