using System;

namespace RockSchool.WebApi.Models.Bands;

public class BandInfo
{
    public Guid BandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
    public object? Teacher { get; set; }
    public int Status { get; set; }
    public BandStudentInfo[]? BandStudents { get; set; }
}