using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class BandMemberRepository : BaseRepository, IBandMemberRepository
{
    public BandMemberRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<BandMember?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.BandMembers
            .Include(bm => bm.Band)
            .Include(bm => bm.Student)
            .FirstOrDefaultAsync(bm => bm.BandMemberId == id);
    }

    public async Task<BandMember[]> GetByBandIdAsync(Guid bandId)
    {
        return await RockSchoolContext.BandMembers
            .Where(bm => bm.BandId == bandId)
            .Include(bm => bm.Student)
            .ToArrayAsync();
    }

    public async Task<BandMember[]> GetByStudentIdAsync(Guid studentId)
    {
        return await RockSchoolContext.BandMembers
            .Where(bm => bm.StudentId == studentId)
            .Include(bm => bm.Band)
                .ThenInclude(b => b.Teacher)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(BandMember bandMember)
    {
        RockSchoolContext.BandMembers.Add(bandMember);
        await RockSchoolContext.SaveChangesAsync();
        return bandMember.BandMemberId;
    }

    public async Task UpdateAsync(BandMember bandMember)
    {
        RockSchoolContext.BandMembers.Update(bandMember);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var bandMember = await RockSchoolContext.BandMembers.FindAsync(id);
        if (bandMember != null)
        {
            RockSchoolContext.BandMembers.Remove(bandMember);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
}
