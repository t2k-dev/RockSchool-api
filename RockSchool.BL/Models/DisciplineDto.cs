using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class DisciplineDto
{
    public int DisciplineId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public ICollection<TeacherDto?> Teachers { get; set; }
}