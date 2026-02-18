using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Students;

namespace RockSchool.BL.Students;

public class StudentScreenDetailsResult
{
    public Student Student { get; set; } = null!;
    public AttendanceWithAttendeesDto[] Attendances { get; set; } = [];
    public Subscription[] Subscriptions { get; set; } = [];
    public Band[] Bands { get; set; } = [];
}
