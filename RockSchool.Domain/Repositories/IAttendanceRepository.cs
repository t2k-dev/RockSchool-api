using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance[]> GetAllAsync();
    Task<Attendance?> GetAsync(Guid attendanceId);
    Task<Attendance[]> GetByRoomIdAsync(int roomId);
    Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Guid> AddAsync(Attendance attendance);
    
    /*
    Task<Attendance[]> GetByBranchIdAsync(int branchId);
    
    Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Attendance[]?> GetByStudentIdAsync(Guid studentId);
    Task<Attendance[]?> GetBySubscriptionIdAsync(Guid subscriptionId);
    
    Task UpdateAsync(Attendance attendance);
    Task DeleteAsync(Guid id);
    Task AddRangeAsync(List<Attendance> attendances);*/
}
