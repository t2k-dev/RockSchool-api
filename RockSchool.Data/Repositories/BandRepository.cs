using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Bands;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories;

public class BandRepository : BaseRepository, IBandRepository
{
    public BandRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<Band?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.Bands
            .Include(b => b.Teacher)
            .Include(b => b.BandStudents!)
                .ThenInclude(bs => bs.Student)
            .FirstOrDefaultAsync(b => b.BandId == id);
    }

    public async Task<Band[]> GetAllAsync()
    {
        return await RockSchoolContext.Bands
            .Include(b => b.Teacher)
            .Include(b => b.BandStudents!)
                .ThenInclude(bs => bs.Student)
            .ToArrayAsync();
    }

    public async Task<Band[]> GetByTeacherIdAsync(Guid teacherId)
    {
        return await RockSchoolContext.Bands
            .Where(b => b.TeacherId == teacherId)
            .Include(b => b.Teacher)
            .Include(b => b.BandStudents!)
                .ThenInclude(bs => bs.Student)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(Band band)
    {
        RockSchoolContext.Bands.Add(band);
        await RockSchoolContext.SaveChangesAsync();
        return band.BandId;
    }

    public async Task UpdateAsync(Band band)
    {
        RockSchoolContext.Bands.Update(band);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var band = await RockSchoolContext.Bands.FindAsync(id);
        if (band != null)
        {
            RockSchoolContext.Bands.Remove(band);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
}