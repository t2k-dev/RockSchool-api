using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class BandRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext), IBandRepository
{
    public async Task<Band?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.Bands
            .Include(b => b.Teacher)
            .Include(b => b.BandMembers!)
                .ThenInclude(bm => bm.Student)
            .FirstOrDefaultAsync(b => b.BandId == id);
    }

    public async Task<Band[]> GetAllAsync()
    {
        return await RockSchoolContext.Bands
            .Include(b => b.Teacher)
            .Include(b => b.BandMembers!)
                .ThenInclude(bm => bm.Student)
            .ToArrayAsync();
    }

    public async Task<Band[]> GetByTeacherIdAsync(Guid teacherId)
    {
        return await RockSchoolContext.Bands
            .Where(b => b.TeacherId == teacherId)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(Band band)
    {
        await RockSchoolContext.Bands.AddAsync(band);
        return band.BandId;
    }

    public void Update(Band band)
    {
        RockSchoolContext.Bands.Update(band);
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