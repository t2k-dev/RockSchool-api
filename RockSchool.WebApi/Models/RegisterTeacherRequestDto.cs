using System;
using RockSchool.Data.Entities;

namespace RockSchool.WebApi.Models;

public class RegisterTeacherRequestDto
{
    public TeacherDto Teacher { get; set; }
    // public WorkingHoursEntity WorkingHoursEntity { get; set; }
}