using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Repositories;

public class AttendanceRepository(RockSchoolContext rockSchoolContext)
{
    public async Task<AttendanceEntity[]> GetAllAsync()
    {
        return await rockSchoolContext.Attendances
            .Include(a => a.Teacher)
            .Include(a => a.Student)
            .ToArrayAsync();
    }

    public async Task<AttendanceEntity?> GetAsync(Guid attendanceId)
    {
        return await rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
    }

    public async Task<AttendanceEntity[]> GetByBranchIdAsync(int branchId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.BranchId == branchId) 
            .Include(a => a.Teacher)
            .Include(a => a.Student)
            .Include(a => a.Subscription)
            //.Include(a => a.Room)
            .ToArrayAsync();
    }

    public async Task<AttendanceEntity[]> GetByRoomIdAsync(int roomId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.RoomId == roomId)
            .Include(a => a.Teacher)
            .Include(a => a.Student)
            .ToArrayAsync();
    }

    public async Task<AttendanceEntity[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.TeacherId == teacherId && a.StartDate >= startDate && a.EndDate <= endDate)
            .Include(a => a.Student)
            .ToArrayAsync();
    }

    public async Task<AttendanceEntity[]?> GetByStudentIdAsync(Guid studentId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.StudentId == studentId)
            .Include(a => a.Teacher)
            .Include(a => a.Subscription)
            .ToArrayAsync();
    }

    public async Task<AttendanceEntity[]?> GetBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.SubscriptionId == subscriptionId)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(AttendanceEntity attendanceEntity)
    {
        await rockSchoolContext.Attendances.AddAsync(attendanceEntity);
        await rockSchoolContext.SaveChangesAsync();
        return attendanceEntity.AttendanceId;
    }

    public async Task UpdateAsync(AttendanceEntity attendanceEntity)
    {
        rockSchoolContext.Attendances.Update(attendanceEntity);
        await rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var attendance = await rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == id);
        if (attendance != null)
        {
            rockSchoolContext.Attendances.Remove(attendance);
            await rockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task AddRangeAsync(List<AttendanceEntity> attendances)
    {
        await rockSchoolContext.Attendances.AddRangeAsync(attendances);
        await rockSchoolContext.SaveChangesAsync();
    }
}