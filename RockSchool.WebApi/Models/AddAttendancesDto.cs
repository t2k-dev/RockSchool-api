using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Models;

public class AddAttendancesDto
{
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int DisciplineId { get; set; }
    public int NumberOfAttendances { get; set; }
    public DateTime StartingDate { get; set; }
}