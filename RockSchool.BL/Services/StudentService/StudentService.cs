using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;

    public StudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> AddStudentAsync(StudentDto studentDto)
    {
        var studentEntity = new StudentEntity
        {
            LastName = studentDto.LastName,
            FirstName = studentDto.FirstName,
            BirthDate = studentDto.BirthDate,
            Phone = studentDto.Phone,
            Sex = studentDto.Sex,
            Level = studentDto.Level
            // UserId = addStudentServiceRequestDto.UserId
        };

        await _studentRepository.AddAsync(studentEntity);

        var savedStudent = await _studentRepository.GetByIdAsync(studentEntity.StudentId);

        if (savedStudent == null)
            throw new InvalidOperationException("Failed to add student.");

        return studentEntity.StudentId;
    }

    public async Task UpdateStudentAsync(StudentDto studentDto)
    {
        var existingStudent = await _studentRepository.GetByIdAsync(studentDto.StudentId);

        if (existingStudent == null)
            throw new NullReferenceException("StudentEntity not found.");

        ModifyStudentAttributes(studentDto, existingStudent);

        await _studentRepository.UpdateAsync(existingStudent);
    }

    public async Task<StudentDto> GetByIdAsync(Guid studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);

        var studentDto = new StudentDto
        {
            StudentId = student.StudentId,
            LastName = student.LastName,
            FirstName = student.FirstName,
            BirthDate = student.BirthDate,
            Phone = student.Phone,
            Sex = student.Sex,

        };

        return studentDto;
    }

    public async Task<StudentDto[]?> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        if (students == null || !students.Any())
            return null;

        var studentDtos = students.Select(s => new StudentDto
        {
            StudentId = s.StudentId,
            LastName = s.LastName,
            FirstName = s.FirstName,
            BirthDate = s.BirthDate,
            Phone = s.Phone,
            Sex = s.Sex,
            // UserId = s.UserId,
            User = s.User
        }).ToArray();

        return studentDtos;
    }

    public async Task DeleteStudentAsync(Guid id)
    {
        var existingStudent = await _studentRepository.GetByIdAsync(id);

        if (existingStudent == null)
            throw new InvalidOperationException("StudentEntity not found.");

        await _studentRepository.DeleteAsync(existingStudent);
    }

    private static void ModifyStudentAttributes(StudentDto updateStudentServiceRequestDto,
        StudentEntity existingStudentEntity)
    {
        if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.FirstName))
            existingStudentEntity.FirstName = updateStudentServiceRequestDto.FirstName;

        if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.LastName))
            existingStudentEntity.LastName = updateStudentServiceRequestDto.LastName;

        if (updateStudentServiceRequestDto.BirthDate != default)
            existingStudentEntity.BirthDate = updateStudentServiceRequestDto.BirthDate;

        if (updateStudentServiceRequestDto.Sex != default)
            existingStudentEntity.Sex = updateStudentServiceRequestDto.Sex;

        if (updateStudentServiceRequestDto.Phone != default)
            existingStudentEntity.Phone = updateStudentServiceRequestDto.Phone;

        // if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.Login))
        //     existingStudentEntity.Login = updateStudentServiceRequestDto.Login;
    }
}