namespace RockSchool.Domain.Teachers
{
    public interface ITeacherRepository
    {
        Task<Teacher[]> GetAllAsync();
        Task AddAsync(Teacher teacher);
        Task<Teacher?> GetByIdAsync(Guid teacherId);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
        Task<Teacher[]> GetTeachersAsync(int branchId, int disciplineId, int studentAge);
        Task<Teacher[]> GetRehearsableTeachersAsync(int branchId);
    }
}
