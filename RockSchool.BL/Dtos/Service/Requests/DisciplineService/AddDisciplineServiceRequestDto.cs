using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Requests.DisciplineService;

public class AddDisciplineServiceRequestDto
{
    public int Id { get; set; }
    public string DisciplineName { get; set; }
    public ICollection<TeacherEntity> Teachers { get; set; }
    public bool IsActive { get; set; }
}