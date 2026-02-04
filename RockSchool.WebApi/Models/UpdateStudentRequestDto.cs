using System;
using RockSchool.Domain.Enums;

namespace RockSchool.WebApi.Models;

public class UpdateStudentRequestDto
{
    public Guid StudentId { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public short Sex { get; set; }
    public long Phone { get; set; }
    public int? Level { get; set; }
}