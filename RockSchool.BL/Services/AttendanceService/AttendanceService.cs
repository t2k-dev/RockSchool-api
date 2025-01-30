using AutoMapper;
using RockSchool.BL.Dtos.Service.Requests.AttendanceService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;
using RockSchool.WebApi.Helpers;

namespace RockSchool.BL.Services.AttendanceService;

public class AttendanceService : IAttendanceService
{
    private readonly AttendanceRepository _attendanceRepository;
    private readonly ScheduleRepository _scheduleRepository;
    protected readonly IMapper _mapper;

    public AttendanceService(AttendanceRepository attendanceRepository, ScheduleRepository scheduleRepository,
        IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<AttendanceDto[]> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();
        var attendancesDto = _mapper.Map<AttendanceDto[]>(attendances);

        return attendancesDto;
    }

    public async Task AddAttendancesToStudent(
        AddAttendanceServiceRequestDto attendanceServiceRequestDto)
    {
        var schedules =
            await _scheduleRepository.GetAllByStudentIdAsync(attendanceServiceRequestDto.StudentId);

        var startDate = attendanceServiceRequestDto.StartingDate;
        var attendancesToAdd = attendanceServiceRequestDto.NumberOfAttendances;
        var newAttendances =
            GenerateAttendances(attendanceServiceRequestDto, attendancesToAdd, schedules, startDate);

        await _attendanceRepository.AddRangeAsync(newAttendances);
    }

    private static List<AttendanceEntity> GenerateAttendances(
        AddAttendanceServiceRequestDto attendanceServiceRequestDto, int attendancesToAdd,
        ScheduleEntity[] schedules, DateTime startDate)
    {
        var newAttendances = new List<AttendanceEntity>();

        while (attendancesToAdd > 0)
        {
            foreach (var item in schedules!)
            {
                var beginDate = ScheduleHelper.GetNextWeekday(startDate, item.WeekDay);

                var attendance = new AttendanceEntity()
                {
                    StudentId = attendanceServiceRequestDto.StudentId,
                    TeacherId = attendanceServiceRequestDto.TeacherId,
                    Status = 1,
                    Duration = item.Duration,
                    BeginDate = beginDate
                };

                newAttendances.Add(attendance);

                attendancesToAdd--;
            }

            startDate = startDate.AddDays(7);
        }

        return newAttendances;
    }
}