using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Teachers;

namespace RockSchool.Data.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly RockSchoolContext _context;

    public TeacherRepository(RockSchoolContext context)
    {
        _context = context;
    }

    public async Task<Teacher[]> GetAllAsync()
    {
        return await _context.Teachers
            .Include(t => t.Disciplines)
            .Include(t => t.WorkingPeriods)
            .Include(t => t.Branch)
            .ThenInclude(b => b.Rooms)
            .ToArrayAsync();
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<Teacher?> GetByIdAsync(Guid teacherId)
    {
        return await _context.Teachers
            .Include(t => t.User)
            .Include(t => t.Disciplines)
            .Include(t => t.WorkingPeriods)
            .Include(t => t.ScheduledWorkingPeriods)
            .FirstOrDefaultAsync(t => t.TeacherId == teacherId);
    }

    public void Update(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
    }

    public async Task DeleteAsync(Teacher teacher)
    {
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<Teacher[]> GetTeachersAsync(int branchId, int disciplineId, int studentAge)
    {
        return await _context.Teachers
            .Include(t => t.WorkingPeriods)
            .Include(t => t.ScheduledWorkingPeriods)
            .Include(t => t.Disciplines)
            .Include(t => t.Branch)
            .Where(t => t.BranchId == branchId 
                        && t.AgeLimit >= studentAge 
                        && t.Disciplines.Any(d => d.DisciplineId == disciplineId)
                        && t.IsActive)
            .AsSplitQuery() 
            .ToArrayAsync();
    }

    public async Task<Teacher[]> GetRehearsableTeachersAsync(int branchId)
    {
        return await _context.Teachers
            .Include(t => t.WorkingPeriods)
            .Include(t => t.ScheduledWorkingPeriods)
            .Include(t => t.Disciplines)
            .Include(t => t.Branch)
            .Where(t => t.BranchId == branchId
                        && t.AllowBands
                        && t.IsActive)
            .AsSplitQuery()
            .ToArrayAsync();
    }
}