using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Responses;

public class DisciplineDto
{
    public int Id { get; set; }
    public string DisciplineName { get; set; }
    public ICollection<TeacherEntity> Teachers { get; set; }
    public bool IsActive { get; set; }
}