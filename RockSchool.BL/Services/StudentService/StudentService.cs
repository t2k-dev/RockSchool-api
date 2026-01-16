using RockSchool.BL.Models;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.UserService;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;
    private readonly BranchRepository _branchRepository;
    private readonly IUserService _userService;

    public StudentService(
        StudentRepository studentRepository,
        BranchRepository branchRepository,
        IUserService userService)
    {
        _studentRepository = studentRepository;
        _branchRepository = branchRepository;
        _userService = userService;
    }

    public async Task<RegisterStudentResponseDto> AddStudentAsync(Student studentDto, string email)
    {
        if (studentDto.BranchId == null)
        {
            return new RegisterStudentResponseDto
            {
                Success = false,
                Message = "BranchId is required",
                StudentId = null,
                UserId = null
            };
        }

        var branchEntity = await _branchRepository.GetByIdAsync(studentDto.BranchId.Value)!;

        if (branchEntity == null)
        {
            return new RegisterStudentResponseDto
            {
                Success = false,
                Message = $"Branch with id {studentDto.BranchId} was not found",
                StudentId = null,
                UserId = null
            };
        }

        // Step 1: Create User account first (RoleId 3 = Student)
        var userResult = await _userService.RegisterUserAsync(email, roleId: 3);

        if (!userResult.Success || !userResult.UserId.HasValue)
        {
            return new RegisterStudentResponseDto
            {
                Success = false,
                Message = $"Failed to create user account: {userResult.Message}",
                StudentId = null,
                UserId = null
            };
        }

        // Step 2: Create Student linked to User
        var studentEntity = new StudentEntity
        {
            UserId = userResult.UserId.Value,
            LastName = studentDto.LastName,
            FirstName = studentDto.FirstName,
            BirthDate = studentDto.BirthDate,
            Phone = studentDto.Phone,
            Sex = studentDto.Sex,
            Level = studentDto.Level,
            Branch = branchEntity
        };

        await _studentRepository.AddAsync(studentEntity);

        var savedStudent = await _studentRepository.GetByIdAsync(studentEntity.StudentId);

        if (savedStudent == null)
        {
            return new RegisterStudentResponseDto
            {
                Success = false,
                Message = "Failed to save student",
                StudentId = null,
                UserId = userResult.UserId
            };
        }

        return new RegisterStudentResponseDto
        {
            Success = true,
            Message = "Student registered successfully. Welcome email sent with login credentials.",
            StudentId = studentEntity.StudentId,
            UserId = userResult.UserId.Value
        };
    }

    public async Task UpdateStudentAsync(Student studentDto)
    {
        // TODO: tempfix
        studentDto.BirthDate = studentDto.BirthDate.ToUniversalTime();

        var existingStudent = await _studentRepository.GetByIdAsync(studentDto.StudentId);

        if (existingStudent == null)
            throw new NullReferenceException("StudentEntity not found.");

        ModifyStudentAttributes(studentDto, existingStudent);

        await _studentRepository.UpdateAsync(existingStudent);
    }

    public async Task<Student> GetByIdAsync(Guid studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);

        var studentDto = new Student
        {
            StudentId = student.StudentId,
            LastName = student.LastName,
            FirstName = student.FirstName,
            BirthDate = student.BirthDate,
            Phone = student.Phone,
            Sex = student.Sex,
            Level = student.Level,
            
        };

        return studentDto;
    }

    public async Task<Student[]?> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        if (students == null || !students.Any())
            return null;

        var studentDtos = students.Select(s => new Student
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

    private static void ModifyStudentAttributes(Student updateStudentServiceRequestDto,
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

        if (updateStudentServiceRequestDto.Level != default)
            existingStudentEntity.Level = updateStudentServiceRequestDto.Level;
        
        // if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.Login))
        //     existingStudentEntity.Login = updateStudentServiceRequestDto.Login;
    }
}