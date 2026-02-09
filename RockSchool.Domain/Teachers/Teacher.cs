using RockSchool.Domain.Entities;
using System.Linq;

namespace RockSchool.Domain.Teachers;

public class Teacher
{
    public Guid TeacherId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int Sex { get; private set; }
    public long Phone { get; private set; }
    public User? User { get; private set; }
    public int BranchId { get; private set; }
    public Branch? Branch { get; private set; }
    public int AgeLimit { get; private set; }
    public bool AllowGroupLessons { get; private set; }
    public bool AllowBands { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<Discipline> _disciplines = new();
    public IReadOnlyCollection<Discipline> Disciplines => _disciplines.AsReadOnly();

    private readonly List<WorkingPeriod> _workingPeriods = new();
    public IReadOnlyCollection<WorkingPeriod> WorkingPeriods => _workingPeriods.AsReadOnly();

    private readonly List<ScheduledWorkingPeriod> _scheduledWorkingPeriods = new();
    public IReadOnlyCollection<ScheduledWorkingPeriod> ScheduledWorkingPeriods => _scheduledWorkingPeriods.AsReadOnly();

    private readonly List<Band> _bands = new();
    public IReadOnlyCollection<Band> Bands => _bands.AsReadOnly();

    private Teacher() { }

    public static Teacher Create(
        string firstName,
        string lastName,
        DateTime birthDate,
        int sex,
        long phone,
        int branchId,
        int ageLimit,
        bool allowGroupLessons = false,
        bool allowBands = false)
    {
        return new Teacher
        {
            TeacherId = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            Sex = sex,
            Phone = phone,
            BranchId = branchId,
            AgeLimit = ageLimit,
            AllowGroupLessons = allowGroupLessons,
            AllowBands = allowBands,
            IsActive = true
        };
    }

    public void UpdateInfo(string firstName, string lastName, DateTime birthDate, int sex, long phone, int ageLimit, bool allowGroupLessons, bool allowBands)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Sex = sex;
        Phone = phone;
        AgeLimit = ageLimit;
        AllowGroupLessons = allowGroupLessons;
        AllowBands = allowBands;
    }

    public void SetActiveStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public void UpdateDisciplines(Discipline[]? disciplines)
    {
        if (disciplines is null)
            return;

        _disciplines.Clear();
        foreach (var discipline in disciplines)
        {
            _disciplines.Add(discipline);
        }
    }

    public void AddWorkingPeriod(WorkingPeriod workingPeriod)
    {
        if (!_workingPeriods.Contains(workingPeriod))
            _workingPeriods.Add(workingPeriod);
    }

    public void UpdateWorkingPeriods(WorkingPeriod[]? newWorkingPeriods)
    {
        if (newWorkingPeriods is null)
            return;

        _workingPeriods.Clear();
        foreach (var workingPeriod in newWorkingPeriods)
        {
            _workingPeriods.Add(workingPeriod);
        }

        //ScheduledWorkingPeriods.ToList().RemoveRange()(swp => swp.StartDate > DateTime.Now);
        // Exclude future scheduled periods that are not actual and add new ones.
        //var scheduledWorkingPeriods = existingTeacher.ScheduledWorkingPeriods.Where(swp => swp.StartDate < DateTime.Now.ToUniversalTime()).ToList();

        //var newScheduledWorkingPeriods = BuildScheduledWorkingPeriods(newWorkingPeriodsEntities, existingTeacher.TeacherId, DateTime.Now, 12);
        //scheduledWorkingPeriods.AddRange(newScheduledWorkingPeriods);

        /*
        existingTeacher.WorkingPeriods = newWorkingPeriodsEntities;
        existingTeacher.ScheduledWorkingPeriods = scheduledWorkingPeriods;
        */
    }

    /// <summary>
    /// Replaces working periods without clearing (use when periods are already deleted from DB)
    /// </summary>
    public void ReplaceWorkingPeriods(WorkingPeriod[]? newWorkingPeriods)
    {
        if (newWorkingPeriods is null)
            return;

        // Don't call Clear() - periods are already deleted from database
        foreach (var workingPeriod in newWorkingPeriods)
        {
            _workingPeriods.Add(workingPeriod);
        }
    }

    public void DeleteWorkingPeriods()
    {
        _workingPeriods.Clear();
    }
}
