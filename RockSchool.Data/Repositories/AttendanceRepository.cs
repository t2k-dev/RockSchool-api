using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories;

public class AttendanceRepository(RockSchoolContext rockSchoolContext)
{
    public async Task<Attendance[]> GetAllAsync()
    {
        return await rockSchoolContext.Attendances
            .Include(a => a.Teacher)
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
                    .ThenInclude(s => s.Student)
            .ToArrayAsync();
    }

    public async Task<Attendance?> GetAsync(Guid attendanceId)
    {
        return await rockSchoolContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
    }

    public async Task<Attendance[]> GetByBranchIdAsync(int branchId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.BranchId == branchId) 
            .Include(a => a.Teacher)
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
                    .ThenInclude(s => s.Student)
            .Include(a => a.Room)
            .ToArrayAsync();
    }

    public async Task<Attendance[]> GetByRoomIdAsync(int roomId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.RoomId == roomId)
            .Include(a => a.Teacher)
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
                    .ThenInclude(s => s.Student)
            .ToArrayAsync();
    }

    public async Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.TeacherId == teacherId && a.StartDate >= startDate && a.EndDate <= endDate)
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
                    .ThenInclude(s => s.Student)
            .ToArrayAsync();
    }

    public async Task<Attendance[]?> GetByStudentIdAsync(Guid studentId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.Attendees.Any(sa => sa.Subscription.StudentId == studentId))
            .Include(a => a.Teacher)
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
            .ToArrayAsync();
    }

    public async Task<Attendance[]?> GetBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await rockSchoolContext.Attendances
            .Where(a => a.Attendees.Any(sa => sa.SubscriptionId == subscriptionId))
            .Include(a => a.Attendees)
                .ThenInclude(sa => sa.Subscription)
            .ToArrayAsync();
    }

    public async Task<Guid> AddAsync(Attendance attendance)
    {
        await rockSchoolContext.Attendances.AddAsync(attendance);
        await rockSchoolContext.SaveChangesAsync();
        return attendance.AttendanceId;
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        rockSchoolContext.Attendances.Update(attendance);
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

    public async Task AddRangeAsync(List<Attendance> attendances)
    {
        await rockSchoolContext.Attendances.AddRangeAsync(attendances);
        await rockSchoolContext.SaveChangesAsync();
    }
}