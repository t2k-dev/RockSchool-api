using System;

namespace RockSchool.WebApi.Models;

public class RegisterStudentRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public short Sex { get; set; }
    public long Phone { get; set; }
    public string Level { get; set; }
}