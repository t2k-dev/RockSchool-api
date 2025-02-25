using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.AttendanceService;

public class AttendanceService : IAttendanceService
{
    private readonly AttendanceRepository _attendanceRepository;
    private readonly ScheduleRepository _scheduleRepository;

    public AttendanceService(AttendanceRepository attendanceRepository, ScheduleRepository scheduleRepository)
    {
        _attendanceRepository = attendanceRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<AttendanceDto[]> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();

        var attendancesDto = attendances.Select(a => new AttendanceDto
        {
            AttendanceId = a.AttendanceId,
            StudentId = a.StudentId,
            Student = a.Student,
            TeacherId = a.TeacherId,
            Teacher = a.Teacher,
            StartDate = a.StartDate,
            Status = a.Status,
            RoomId = a.RoomId,
            Room = a.Room,
            EndDate = a.EndDate,
            Comment = a.Comment
        }).ToArray();

        return attendancesDto;
    }

    public async Task AddAttendancesToStudent(AttendanceDto attendanceServiceRequestDto)
    {
        var schedules =
            await _scheduleRepository.GetAllByStudentIdAsync(attendanceServiceRequestDto.StudentId);

        var startDate = attendanceServiceRequestDto.StartDate;
        var attendancesToAdd = attendanceServiceRequestDto.NumberOfAttendances;
        var newAttendances =
            GenerateAttendances(attendanceServiceRequestDto, attendancesToAdd, schedules, startDate);

        await _attendanceRepository.AddRangeAsync(newAttendances);
    }

    private static List<AttendanceEntity> GenerateAttendances(
        AttendanceDto attendanceServiceRequestDto, int attendancesToAdd,
        ScheduleEntity[] schedules, DateTime startDate)
    {
        var newAttendances = new List<AttendanceEntity>();

        while (attendancesToAdd > 0)
        {
            foreach (var scheduleEntity in schedules!)
            {
                var beginDate = ScheduleHelper.GetNextWeekday(startDate, scheduleEntity.WeekDay);

                var attendance = new AttendanceEntity
                {
                    StudentId = attendanceServiceRequestDto.StudentId,
                    TeacherId = attendanceServiceRequestDto.TeacherId,
                    Status = AttendanceStatus.New,
                    // TODO: day of week and time, add logic, discuss logic!!
                    EndDate = scheduleEntity.EndTime,
                    StartDate = beginDate
                };

                newAttendances.Add(attendance);

                attendancesToAdd--;
            }

            startDate = startDate.AddDays(7);
        }

        return newAttendances;
    }
}