using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Students;

public class Student
{
    public Guid StudentId { get; private set; }
    public User? User { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public short Sex { get; private set; }
    public DateTime BirthDate { get; private set; }
    public long Phone { get; private set; }
    public int? Level { get; private set; }
    public Branch? Branch { get; private set; }
    public bool IsWaiting { get; private set; }

    private readonly List<BandStudent> _bandStudents = new();
    public IReadOnlyCollection<BandStudent> BandStudents => _bandStudents.AsReadOnly();

    private Student() { }

    public static Student Create(
        string firstName,
        string? lastName,
        short sex,
        DateTime birthDate,
        long phone,
        int? level = null)
    {
        return new Student
        {
            StudentId = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Sex = sex,
            BirthDate = birthDate,
            Phone = phone,
            Level = level,
            IsWaiting = false
        };
    }

    public void UpdateInfo(string firstName, string? lastName, short sex, DateTime birthDate, long phone, int? level)
    {
        FirstName = firstName;
        LastName = lastName;
        Sex = sex;
        BirthDate = birthDate;
        Phone = phone;
        Level = level;
    }

    public void SetWaitingStatus(bool isWaiting)
    {
        IsWaiting = isWaiting;
    }
}
