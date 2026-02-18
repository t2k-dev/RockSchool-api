namespace RockSchool.BL.Students;

public interface IStudentScreenDetailsService
{
    Task<StudentScreenDetailsResult> Query(Guid studentId);
}
