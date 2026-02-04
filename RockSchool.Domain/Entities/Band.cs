namespace RockSchool.Domain.Entities;

public class Band
{
    public Guid BandId { get; private set; }
    public string Name { get; private set; }
    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; }
    public int Status { get; private set; }

    private readonly List<BandStudent> _bandStudents = new();
    private readonly List<Schedule> _schedules = new();

    public IReadOnlyCollection<BandStudent> BandStudents => _bandStudents.AsReadOnly();
    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();

    private Band() { }

    public static Band Create(string name, Guid teacherId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Band name is required", nameof(name));

        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        return new Band
        {
            BandId = Guid.NewGuid(),
            Name = name,
            TeacherId = teacherId,
            Status = 1 // Active
        };
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Band name is required", nameof(name));

        Name = name;
    }

    public void ChangeTeacher(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        TeacherId = teacherId;
    }

    public void SetStatus(int status)
    {
        Status = status;
    }

    public void AddStudent(Student student, int bandRoleId)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        if (_bandStudents.Any(bs => bs.StudentId == student.StudentId))
            throw new InvalidOperationException("Student is already a member of this band");

        var bandStudent = BandStudent.Create(BandId, student.StudentId, bandRoleId);
        _bandStudents.Add(bandStudent);
    }

    public void RemoveStudent(Guid studentId)
    {
        var bandStudent = _bandStudents.FirstOrDefault(bs => bs.StudentId == studentId);
        if (bandStudent != null)
        {
            _bandStudents.Remove(bandStudent);
        }
    }
}
