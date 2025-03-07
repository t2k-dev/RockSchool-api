using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class TeacherRepository
{
    private readonly RockSchoolContext _context;

    public TeacherRepository(RockSchoolContext context)
    {
        _context = context;
    }

    public async Task<TeacherEntity[]> GetAllAsync()
    {
        return await _context.Teachers
            .Include(t => t.Disciplines)
            .Include(t => t.WorkingPeriods)
            .Include(t => t.Branch)
            .ThenInclude(b => b.Rooms)
            .ToArrayAsync();
    }

    public async Task AddAsync(TeacherEntity teacherEntity)
    {
        await _context.Teachers.AddAsync(teacherEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<TeacherEntity?> GetByIdAsync(Guid teacherId)
    {
        return await _context.Teachers
            .Include(t => t.User)
            .Include(t => t.Disciplines)
            .Include(t => t.WorkingPeriods)
            .FirstOrDefaultAsync(t => t.TeacherId == teacherId);
    }

    public async Task UpdateAsync(TeacherEntity teacherEntity)
    {
        _context.Teachers.Update(teacherEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TeacherEntity teacherEntity)
    {
        _context.Teachers.Remove(teacherEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<TeacherEntity[]> GetTeachersByBranchIdAndDisciplineIdAsync(int branchId, int disciplineId)
    {
        return await _context.Teachers
            .Include(t => t.WorkingPeriods)
            .Include(t => t.Disciplines)
            .Include(t => t.Branch)
            .Where(t => t.BranchId == branchId && t.Disciplines.Any(d => d.DisciplineId == disciplineId))
            .AsSplitQuery() 
            .ToArrayAsync();
    }
}