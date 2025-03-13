using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.AvailablePeriodsService;

public class AvailablePeriodsService : IAvailablePeriodsService
{
    private readonly TeacherRepository _teacherRepository;

    public AvailablePeriodsService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<AvailablePeriodsDto?> GetAvailablePeriodsAsync(int disciplineId, int branchId)
    {
        var teachersEntities =
            await _teacherRepository.GetTeachersByBranchIdAndDisciplineIdAsync(branchId, disciplineId);

        if (teachersEntities.Length == 0)
            return null;

        var periods = CreatePeriodsArray(teachersEntities);
        var teachers = CreateTeachersArray(teachersEntities);

        return new AvailablePeriodsDto()
        {
            Periods = periods,
            Teachers = teachers
        };
    }

    private string[] CreatePeriodsArray(TeacherEntity[] teacherEntities)
    {
        var result = new List<string>();

        foreach (var teacher in teacherEntities)
        {
            if (teacher.WorkingPeriods == null) 
                continue;

            foreach (var period in teacher.WorkingPeriods)
            {
                var dayName = MapWeekDay(period.WeekDay);

                var startTime = TimeSpanToFormattedString(period.StartTime);
                var endTime = TimeSpanToFormattedString(period.EndTime);

                var periodStr = $"{teacher.FirstName}: {dayName}: {startTime} - {endTime}";
                result.Add(periodStr);
            }
        }

        return result.ToArray();
    }

    private static string TimeSpanToFormattedString(TimeSpan time)
    {
        return $"{time.Hours:D2}:{time.Minutes:D2}";
    }

    private object[] CreateTeachersArray(TeacherEntity[] teacherEntities)
    {
        return teacherEntities
            .Select(t => new
            {
                TeacherId = t.TeacherId.ToString(),
                FullName = $"{t.FirstName} {t.LastName}"
            })
            .ToArray<object>();
    }

    private string MapWeekDay(int day) => day switch
    {
        1 => "Пн",
        2 => "Вт",
        3 => "Ср",
        4 => "Чт",
        5 => "Пт",
        6 => "Сб",
        0 => "Вс",
        _ => "N/A"
    };
}