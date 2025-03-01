using System;
using RockSchool.Data.Entities;

namespace RockSchool.WebApi.Models;

public class TeacherDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int[] Disciplines { get; set; }
    public int? UserId { get; set; }
    public int? BranchId { get; set; }
}