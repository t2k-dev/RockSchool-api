using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Students;

namespace RockSchool.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly RockSchoolContext _context;

    public StudentRepository(RockSchoolContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetByIdAsync(Guid studentId)
    {
        return await _context.Students
            .Include(s => s.Branch)
            .SingleOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    public async Task<Student[]> GetAllAsync()
    {
        return await _context.Students.ToArrayAsync();
    }
}