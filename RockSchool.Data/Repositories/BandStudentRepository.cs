using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class BandStudentRepository : BaseRepository, IBandStudentRepository
{
    public BandStudentRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<BandStudent?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.BandStudents
            .Include(bs => bs.Band)
            .Include(bs => bs.Student)
            .FirstOrDefaultAsync(bs => bs.BandStudentId == id);
    }

    public async Task<BandStudent[]> GetByBandIdAsync(Guid bandId)
    {
        return await RockSchoolContext.BandStudents
            .Where(bs => bs.BandId == bandId)
            .Include(bs => bs.Student)
            .ToArrayAsync();
    }

    public async Task<BandStudent[]> GetByStudentIdAsync(Guid studentId)
    {
        return await RockSchoolContext.BandStudents
            .Where(bs => bs.StudentId == studentId)
            .Include(bs => bs.Band)
                .ThenInclude(b => b.Teacher)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(BandStudent bandStudent)
    {
        RockSchoolContext.BandStudents.Add(bandStudent);
        await RockSchoolContext.SaveChangesAsync();
        return bandStudent.BandStudentId;
    }

    public async Task UpdateAsync(BandStudent bandStudent)
    {
        RockSchoolContext.BandStudents.Update(bandStudent);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var bandStudent = await RockSchoolContext.BandStudents.FindAsync(id);
        if (bandStudent != null)
        {
            RockSchoolContext.BandStudents.Remove(bandStudent);
            await RockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task RemoveStudentFromBandAsync(Guid bandId, Guid studentId)
    {
        var bandStudent = await RockSchoolContext.BandStudents
            .FirstOrDefaultAsync(bs => bs.BandId == bandId && bs.StudentId == studentId);
        
        if (bandStudent != null)
        {
            RockSchoolContext.BandStudents.Remove(bandStudent);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
}