using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class StudentRepository
{
    private readonly RockSchoolContext _context;

    public StudentRepository(RockSchoolContext context)
    {
        _context = context;
    }

    public async Task AddAsync(StudentEntity studentEntity)
    {
        await _context.Students.AddAsync(studentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<StudentEntity?> GetByIdAsync(Guid studentId)
    {
        return await _context.Students.SingleOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task UpdateAsync(StudentEntity studentEntity)
    {
        _context.Students.Update(studentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(StudentEntity studentEntity)
    {
        _context.Students.Remove(studentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<StudentEntity[]> GetAllAsync()
    {
        return await _context.Students.ToArrayAsync();
    }
}