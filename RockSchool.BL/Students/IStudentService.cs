using RockSchool.BL.Common.Models;
using RockSchool.Domain.Students;

namespace RockSchool.BL.Students;

public interface IStudentService
{
    Task UpdatePersonalDataAsync(Guid studentId, PersonalDataDto studentDto);
    Task<Student[]?> GetAllStudentsAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task DeleteStudentAsync(Guid id);
}