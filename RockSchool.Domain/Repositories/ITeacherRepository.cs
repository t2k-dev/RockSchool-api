using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Repositories;

public interface ITeacherRepository
{
    Task<Teacher[]> GetAllAsync();
    Task AddAsync(Teacher teacher);
    Task<Teacher?> GetByIdAsync(Guid teacherId);
    void Update(Teacher teacher);
    Task DeleteAsync(Teacher teacher);
    Task<Teacher[]> GetTeachersAsync(int branchId, int disciplineId, int studentAge, DateTime startDate);
    Task<Teacher[]> GetRehearsableTeachersAsync(int branchId);
}
