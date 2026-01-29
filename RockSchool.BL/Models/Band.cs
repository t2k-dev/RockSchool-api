namespace RockSchool.BL.Models;

public class Band
{
    public Guid BandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
    public virtual Teacher? Teacher { get; set; }
    public int Status { get; set; }
    public virtual ICollection<BandStudent>? BandStudents { get; set; }
}