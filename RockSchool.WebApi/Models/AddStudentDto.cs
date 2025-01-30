using System;
using RockSchool.WebApi.Models.Enums;

namespace RockSchool.WebApi.Models;

public class AddStudentDto
{
    public required string FirstName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Sex { get; set; }
    public long Phone { get; set; }
    public StudentLevel Level { get; set; }
}