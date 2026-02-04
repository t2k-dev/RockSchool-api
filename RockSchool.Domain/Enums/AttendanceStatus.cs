namespace RockSchool.Domain.Enums;

public enum AttendanceStatus
{
    New = 1,
    Attended = 2,
    Missed = 3,
    CanceledByStudent = 4,
    CanceledByAdmin = 5,
    CanceledByTeacher = 6
}