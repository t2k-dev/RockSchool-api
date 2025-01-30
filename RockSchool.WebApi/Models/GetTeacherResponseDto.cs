using System;

namespace RockSchool.WebApi.Models;

public class GetTeacherResponseDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int[] Disciplines { get; set; }
}