namespace RockSchool.BL.Dtos.Service.Requests.DisciplineService;

public class UpdateDisciplineServiceRequestDto
{
    public int Id { get; set; }

    public string DisciplineName { get; set; }

    public bool IsActive { get; set; }
}