namespace RockSchool.BL.Students.AddStudent;

public interface IAddStudentService
{
    Task<Guid> Handle(AddStudentDto request);
}
