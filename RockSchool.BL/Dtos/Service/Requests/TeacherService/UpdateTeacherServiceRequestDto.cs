using System.ComponentModel.DataAnnotations.Schema;
using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Requests.TeacherService;

public class UpdateTeacherServiceRequestDto
{
    public int TeacherId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int[] Disciplines { get; set; }
    public int? UserId { get; set; }
}