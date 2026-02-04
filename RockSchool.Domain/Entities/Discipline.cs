namespace RockSchool.Domain.Entities;

public class Discipline
{
    public int DisciplineId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<RoomDiscipline> _roomDisciplines = new();
    private readonly List<TeacherDiscipline> _teacherDisciplines = new();

    public IReadOnlyCollection<RoomDiscipline> RoomDisciplines => _roomDisciplines.AsReadOnly();
    public IReadOnlyCollection<TeacherDiscipline> TeacherDisciplines => _teacherDisciplines.AsReadOnly();

    private Discipline() { }

    public static Discipline Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Discipline name is required", nameof(name));

        return new Discipline
        {
            Name = name,
            IsActive = true
        };
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Discipline name is required", nameof(name));

        Name = name;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
