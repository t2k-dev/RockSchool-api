using RockSchool.Domain.Repositories;
using RockSchool.Domain.Students;

namespace RockSchool.BL.Students.AddStudent;

public class AddStudentService(IStudentRepository studentRepository) : IAddStudentService
{
    public async Task<Guid> Handle(AddStudentDto request)
    {
        var student = Student.Create(
            request.FirstName,
            request.LastName,
            request.Sex,
            request.BirthDate,
            request.Phone,
            request.Level
        );

        // Set branch if provided
        if (request.BranchId > 0)
        {
            // TODO: Set branch appropriately if Student entity supports it
            // For now, we may need to handle branch assignment differently
        }

        await studentRepository.AddAsync(student);

        return student.StudentId;
    }
}
