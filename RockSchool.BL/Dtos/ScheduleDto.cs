using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }
    public SubscriptionDto? Subscription { get; set; }
    public int WeekDay { get; set; }
    // TODO: Пробегаю по массиву скечюдей и начинаю именно c наименьшего weekday и starttime, i = attendanceCount
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}