using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class AttendanceRepository
{
    private RockSchoolContext _rockSchoolContext;
    
    public AttendanceRepository(RockSchoolContext rockSchoolContext)
    {
        _rockSchoolContext = rockSchoolContext;
    }

    public async Task<AttendanceEntity[]> GetAllAsync()
    {
        return await _rockSchoolContext.Attendances.ToArrayAsync();
    }

    public async Task<AttendanceEntity?> GetByIdAsync(int id)
    {
        return await _rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == id);
    }

    public async Task AddAsync(AttendanceEntity attendanceEntity)
    {
        await _rockSchoolContext.Attendances.AddAsync(attendanceEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AttendanceEntity attendanceEntity)
    {
        _rockSchoolContext.Attendances.Update(attendanceEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attendance = await _rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == id);
        if (attendance != null)
        {
            _rockSchoolContext.Attendances.Remove(attendance);
            await _rockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task AddRangeAsync(List<AttendanceEntity> attendances)
    {
        await _rockSchoolContext.Attendances.AddRangeAsync(attendances);
        await _rockSchoolContext.SaveChangesAsync();
    }
}