using RockSchool.BL.Common.Models;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Students;

namespace RockSchool.BL.Students;

public class StudentService(
    IStudentRepository studentRepository, 
    IBranchRepository branchRepository,
    IUnitOfWork unitOfWork
    ) : IStudentService
{

    public async Task UpdatePersonalDataAsync(Guid studentId, PersonalDataDto studentDto)
    {
        var student = await studentRepository.GetByIdAsync(studentId);

        student.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Sex, studentDto.BirthDate, studentDto.Phone, studentDto.Level);

        studentRepository.Update(student);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Student> GetByIdAsync(Guid studentId)
    {
        return await studentRepository.GetByIdAsync(studentId);
    }

    public async Task<Student[]?> GetAllStudentsAsync()
    {
        var students = await studentRepository.GetAllAsync();

        if (students == null || !students.Any())
            return null;

        return students;
    }

    public async Task DeleteStudentAsync(Guid id)
    {
        var existingStudent = await studentRepository.GetByIdAsync(id);

        if (existingStudent == null)
            throw new InvalidOperationException("StudentEntity not found.");

        await studentRepository.DeleteAsync(existingStudent);
    }
}