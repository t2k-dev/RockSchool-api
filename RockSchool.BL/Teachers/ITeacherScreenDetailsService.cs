namespace RockSchool.BL.Teachers;

public interface ITeacherScreenDetailsService
{
    Task<TeacherScreenDetailsResult> Query(Guid teacherId);
}