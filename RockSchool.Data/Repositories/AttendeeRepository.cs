using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class AttendeeRepository : BaseRepository, IAttendeeRepository
{
    public AttendeeRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddAsync(Attendee attendee)
    {
        await RockSchoolContext.Attendees.AddAsync(attendee);
    }


    /*
    public async Task<Attendee?> GetByIdAsync(Guid id)
    {
        return await RockSchoolContext.Attendees
            .Include(sa => sa.Subscription)
            .Include(sa => sa.Attendance)
            .FirstOrDefaultAsync(sa => sa.SubscriptionAttendanceId == id);
    }

    public async Task<Attendee[]> GetBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await RockSchoolContext.Attendees
            .Where(sa => sa.SubscriptionId == subscriptionId)
            .Include(sa => sa.Attendance)
            .ToArrayAsync();
    }

    public async Task<Attendee[]> GetByAttendanceIdAsync(Guid attendanceId)
    {
        return await RockSchoolContext.Attendees
            .Where(sa => sa.AttendanceId == attendanceId)
            .Include(sa => sa.Subscription)
            .ToArrayAsync();
    }


    public async Task UpdateAsync(Attendee attendee)
    {
        RockSchoolContext.Attendees.Update(attendee);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var subscriptionsAttendances = await RockSchoolContext.Attendees.FindAsync(id);
        if (subscriptionsAttendances != null)
        {
            RockSchoolContext.Attendees.Remove(subscriptionsAttendances);
            await RockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task DeleteBySubscriptionAndAttendanceAsync(Guid subscriptionId, Guid attendanceId)
    {
        var subscriptionsAttendances = await RockSchoolContext.Attendees
            .FirstOrDefaultAsync(sa => sa.SubscriptionId == subscriptionId && sa.AttendanceId == attendanceId);
        
        if (subscriptionsAttendances != null)
        {
            RockSchoolContext.Attendees.Remove(subscriptionsAttendances);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
    */
}