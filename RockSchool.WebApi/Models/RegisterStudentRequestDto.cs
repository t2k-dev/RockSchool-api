using System;
using RockSchool.Data.Enums;

namespace RockSchool.WebApi.Models;

public class RegisterStudentRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public short Sex { get; set; }
    public long Phone { get; set; }
    public int Level { get; set; }
    public int BranchId { get; set; }
}