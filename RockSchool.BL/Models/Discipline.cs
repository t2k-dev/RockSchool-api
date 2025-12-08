namespace RockSchool.BL.Models;

public class Discipline
{
    public int DisciplineId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Teacher?> Teachers { get; set; }
}