using System;

namespace RockSchool.WebApi.Models;

public class AddAttendancesDto
{
    public Guid StudentId { get; set; }
    public Guid TeacherId { get; set; }
    public int DisciplineId { get; set; }
    public int NumberOfAttendances { get; set; }
    public DateTime StartingDate { get; set; }
}