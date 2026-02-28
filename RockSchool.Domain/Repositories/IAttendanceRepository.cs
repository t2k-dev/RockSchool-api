using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance[]> GetAllAsync();
    Task<Attendance?> GetAsync(Guid attendanceId);
    Task<Attendance[]?> GetBySubscriptionIdAsync(Guid subscriptionId);
    Task<Attendance[]> GetByBranchIdAsync(int branchId);
    Task<Attendance[]> GetByRoomIdAsync(int roomId);
    Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Guid> AddAsync(Attendance attendance);
    void Update(Attendance attendance);
    Task DeleteAsync(Guid id);

    
    /*
    Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Attendance[]?> GetByStudentIdAsync(Guid studentId);
    Task<Attendance[]?> GetBySubscriptionIdAsync(Guid subscriptionId);
    
    
    
    Task AddRangeAsync(List<Attendance> attendances);*/
}
