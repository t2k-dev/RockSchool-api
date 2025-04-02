using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Repositories;

public class AttendanceRepository
{
    private readonly RockSchoolContext _rockSchoolContext;

    public AttendanceRepository(RockSchoolContext rockSchoolContext)
    {
        _rockSchoolContext = rockSchoolContext;
    }

    public async Task<AttendanceEntity[]> GetAllAsync()
    {
        return await _rockSchoolContext.Attendances.ToArrayAsync();
    }

    public async Task<AttendanceEntity?> GetByIdAsync(Guid id)
    {
        return await _rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == id);
    }

    public async Task<Guid> AddAsync(AttendanceEntity attendanceEntity)
    {
        await _rockSchoolContext.Attendances.AddAsync(attendanceEntity);
        await _rockSchoolContext.SaveChangesAsync();
        return attendanceEntity.AttendanceId;
    }

    public async Task UpdateAsync(AttendanceEntity attendanceEntity)
    {
        _rockSchoolContext.Attendances.Update(attendanceEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
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

    public async Task<AttendanceEntity[]?> GetAttendancesByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate,
        AttendanceStatus status)
    {
        return await _rockSchoolContext.Attendances.Where(a =>
            a.TeacherId == teacherId && a.StartDate >= startDate && a.EndDate <= endDate && a.Status == status).ToArrayAsync();
    }

    public async Task<AttendanceEntity[]?> GetAttendancesByStudentIdAsync(Guid studentId)
    {
        return await _rockSchoolContext.Attendances
            .Where(a => a.StudentId == studentId)
            .Include(a => a.Teacher)
            .ToArrayAsync();
    }
}