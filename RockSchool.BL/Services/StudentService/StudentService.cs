using RockSchool.Data.Repositories;
using RockSchool.Domain.Branches;
using RockSchool.Domain.Students;

namespace RockSchool.BL.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IBranchRepository _branchRepository;

    public StudentService(IStudentRepository studentRepository, IBranchRepository branchRepository)
    {
        _studentRepository = studentRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Guid> AddStudentAsync(Student studentDto)
    {
        if (studentDto.Branch?.BranchId == null)
            throw new NullReferenceException("BranchId is required.");

        var branchEntity = await _branchRepository.GetByIdAsync(studentDto.Branch.BranchId)!;

        if (branchEntity == null)
            throw new NullReferenceException($"Branch with id {studentDto.Branch.BranchId} was not found.");



        var studentEntity = Student.Create(studentDto.FirstName, studentDto.LastName, studentDto.Sex,
            studentDto.BirthDate, studentDto.Phone);

        await _studentRepository.AddAsync(studentEntity);

        var savedStudent = await _studentRepository.GetByIdAsync(studentEntity.StudentId);

        if (savedStudent == null)
            throw new InvalidOperationException("Failed to add student.");

        return studentEntity.StudentId;
    }

    public async Task UpdateStudentAsync(Student studentDto)
    {
        throw new NotImplementedException();
        /*
        // TODO: tempfix
        //studentDto.BirthDate = studentDto.BirthDate.ToUniversalTime();

        var existingStudent = await _studentRepository.GetByIdAsync(studentDto.StudentId);

        if (existingStudent == null)
            throw new NullReferenceException("StudentEntity not found.");


        ModifyStudentAttributes(studentDto, existingStudent);

        await _studentRepository.UpdateAsync(existingStudent);*/
    }

    public async Task<Student> GetByIdAsync(Guid studentId)
    {
        return await _studentRepository.GetByIdAsync(studentId);
    }

    public async Task<Student[]?> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        if (students == null || !students.Any())
            return null;

        return students;
    }

    public async Task DeleteStudentAsync(Guid id)
    {
        var existingStudent = await _studentRepository.GetByIdAsync(id);

        if (existingStudent == null)
            throw new InvalidOperationException("StudentEntity not found.");

        await _studentRepository.DeleteAsync(existingStudent);
    }
}