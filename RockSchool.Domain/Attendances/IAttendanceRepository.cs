using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Attendances
{
    public interface IAttendanceRepository
    {
        Task<Attendance[]> GetAllAsync();
        Task<Attendance?> GetAsync(Guid attendanceId);
        Task<Attendance[]> GetByBranchIdAsync(int branchId);
        Task<Attendance[]> GetByRoomIdAsync(int roomId);
        Task<Attendance[]?> GetByTeacherIdForPeriodOfTimeAsync(Guid teacherId, DateTime startDate, DateTime endDate);
        Task<Attendance[]?> GetByStudentIdAsync(Guid studentId);
        Task<Attendance[]?> GetBySubscriptionIdAsync(Guid subscriptionId);
        Task<Guid> AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(Guid id);
        Task AddRangeAsync(List<Attendance> attendances);
    }
}
