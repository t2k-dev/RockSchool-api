namespace RockSchool.BL.Models.Dtos;

public class AttendanceWithAttendeesDto
{
    public Guid AttendanceId { get; set; }
    public Guid TeacherId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public List<AttendeeDto> Attendees { get; set; } = new();
}

public class AttendeeDto
{
    public Guid AttendeeId { get; set; }
    public Guid SubscriptionId { get; set; }
    public Guid StudentId { get; set; }
    public int Status { get; set; }
    public StudentInfoDto Student { get; set; } = null!;
    public SubscriptionInfoDto Subscription { get; set; } = null!;
}

public class StudentInfoDto
{
    public Guid StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class SubscriptionInfoDto
{
    public Guid SubscriptionId { get; set; }
    public Guid? GroupId { get; set; }
    public int AttendanceCount { get; set; }
    public int AttendanceLength { get; set; }
    public int AttendancesLeft { get; set; }
    public DateOnly StartDate { get; set; }
    public int Status { get; set; }
    public int SubscriptionType { get; set; }
    public int? DisciplineId { get; set; }
}
