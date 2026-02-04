using RockSchool.BL.Services.ScheduledWorkingPeriodsService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly DisciplineRepository _disciplineRepository;
    private readonly TeacherRepository _teacherRepository;
    private readonly IScheduledWorkingPeriodsService _scheduledWorkingPeriodsService;

    public TeacherService(TeacherRepository teacherRepository, DisciplineRepository disciplineRepository,
        IScheduledWorkingPeriodsService scheduledWorkingPeriodsService)
    {
        _teacherRepository = teacherRepository;
        _disciplineRepository = disciplineRepository;
        _scheduledWorkingPeriodsService = scheduledWorkingPeriodsService;
    }

    

    public async Task<Teacher[]> GetAllTeachersAsync()
    {
        var teacherEntities = await _teacherRepository.GetAllAsync();
        if (!teacherEntities.Any())
        {
            return [];
        }

        return teacherEntities;
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid id)
    {
        throw new NotImplementedException();
        /*
        var teacherEntity = await _teacherRepository.GetByIdAsync(id);
        if (teacherEntity == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        var disciplines = teacherEntity.Disciplines.Select(teacherDiscipline => new Discipline
            {
                Name = teacherDiscipline.Name, 
                DisciplineId = teacherDiscipline.DisciplineId, 
                IsActive = teacherDiscipline.IsActive, 
                // DEV Teachers = teacherDiscipline.Teachers,
            })
            .ToList();
        
        var teacher = new Teacher
        {
            TeacherId = teacherEntity.TeacherId,
            LastName = teacherEntity.LastName,
            FirstName = teacherEntity.FirstName,
            BirthDate = teacherEntity.BirthDate,
            Sex = teacherEntity.Sex,
            Phone = teacherEntity.Phone,
            User = teacherEntity.User,
            Disciplines = disciplines,
            // DEV DisciplineIds = disciplines.Select(d => d.DisciplineId).ToArray(),
            AllowGroupLessons = teacherEntity.AllowGroupLessons,
            AllowBands = teacherEntity.AllowBands,
            AgeLimit = teacherEntity.AgeLimit,
            IsActive = teacherEntity.IsActive,
            BranchId = teacherEntity.BranchId,
            WorkingPeriods = teacherEntity.WorkingPeriods,
            ScheduledWorkingPeriods = teacherEntity.ScheduledWorkingPeriods,
        };

        return teacher;*/
    }

    public async Task<Teacher[]?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
    {
        return await _teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge);
    }

    public async Task<Teacher[]?> GetRehearsableTeachersAsync(int branchId)
    {
        return await _teacherRepository.GetRehearsableTeachersAsync(branchId);
    }

    public async Task<Guid> AddTeacher(Teacher addTeacherDto)
    {
        throw new NotImplementedException();
        /*
        var disciplines = await _disciplineRepository.GetByIdsAsync(addTeacherDto.DisciplineIds);
        var workingPeriodEntities = addTeacherDto.WorkingPeriods;
        var scheduledWorkingPeriods = BuildScheduledWorkingPeriods(workingPeriodEntities, addTeacherDto.TeacherId, DateTime.Now.ToUniversalTime(), 3);

        // Teacher
        var teacherEntity = new TeacherEntity
        {
            LastName = addTeacherDto.LastName,
            FirstName = addTeacherDto.FirstName,
            BirthDate = addTeacherDto.BirthDate,
            Phone = addTeacherDto.Phone,
            BranchId = addTeacherDto.BranchId.Value,
            Disciplines = disciplines,
            WorkingPeriods = workingPeriodEntities,
            ScheduledWorkingPeriods = scheduledWorkingPeriods,
            Sex = addTeacherDto.Sex,
            AllowGroupLessons = addTeacherDto.AllowGroupLessons,
            AllowBands = addTeacherDto.AllowBands,
            AgeLimit = addTeacherDto.AgeLimit,
            IsActive = addTeacherDto.IsActive,
        };

        await _teacherRepository.AddAsync(teacherEntity);

        return teacherEntity.TeacherId;*/
    }

    public async Task UpdateTeacherAsync(Teacher teacherDto, bool updateDisciplines, bool recalculatePeriods)
    {
        throw new NotImplementedException();
        /*var existingTeacher = await _teacherRepository.GetByIdAsync(teacherDto.TeacherId);
        if (existingTeacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {teacherDto.TeacherId} was not found.");
        }

        // Disciplines
        if (updateDisciplines)
        {
            existingTeacher.Disciplines.Clear();
            existingTeacher.Disciplines = await _disciplineRepository.GetByIdsAsync(teacherDto.DisciplineIds);
        }

        // Periods
        if (recalculatePeriods)
        {
            UpdateTeacherWorkingPeriods(existingTeacher, teacherDto.WorkingPeriods.ToList());
        }

        UpdateTeacherDetails(teacherDto, existingTeacher);

        await _teacherRepository.UpdateAsync(existingTeacher);*/
    }

    public async Task SetTeacherActiveAsync(Guid id, bool isActive)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(id);
        if (existingTeacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        existingTeacher.SetActiveStatus(isActive);

        await _teacherRepository.UpdateAsync(existingTeacher);
    }

    public async Task DeleteTeacherAsync(Guid id)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(id);
        if (existingTeacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        await _teacherRepository.DeleteAsync(existingTeacher);
    }

    private List<ScheduledWorkingPeriod> BuildScheduledWorkingPeriods(List<WorkingPeriod> workingPeriodEntities, Guid teacherId, DateTime startDate, int months)
    {
        var scheduledWorkingPeriodEntities = new List<ScheduledWorkingPeriod>();

        startDate = startDate.Date;
        var endDate = startDate.AddMonths(months);

        for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
        {
            foreach (var workingPeriodEntity in workingPeriodEntities)
            {
                if ((int)currentDate.DayOfWeek == workingPeriodEntity.WeekDay)
                {
                    var localStart = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.StartTime), DateTimeKind.Local);
                    var localEnd = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.EndTime), DateTimeKind.Local);

                    var periodStart = localStart.ToUniversalTime();
                    var periodEnd = localEnd.ToUniversalTime();

                    var scheduledWorkingPeriod = ScheduledWorkingPeriod.Create(teacherId, periodStart, periodEnd, workingPeriodEntity.RoomId);
                    scheduledWorkingPeriodEntities.Add(scheduledWorkingPeriod);
                }
            }
        }

        return scheduledWorkingPeriodEntities;
    }

    private void UpdateTeacherWorkingPeriods(Teacher existingTeacher, List<WorkingPeriod> workingPeriods)
    {
        var newWorkingPeriodsEntities = workingPeriods;

        //existingTeacher.ScheduledWorkingPeriods.ToList().RemoveRange() (swp => swp.StartDate > DateTime.Now)
        // Exclude future scheduled periods that are not actual and add new ones.
        var scheduledWorkingPeriods = existingTeacher.ScheduledWorkingPeriods.Where(swp => swp.StartDate < DateTime.Now.ToUniversalTime()).ToList();

        var newScheduledWorkingPeriods = BuildScheduledWorkingPeriods(newWorkingPeriodsEntities, existingTeacher.TeacherId, DateTime.Now, 12);
        scheduledWorkingPeriods.AddRange(newScheduledWorkingPeriods);

        // DEV existingTeacher.WorkingPeriods. Clear();
        /*
        existingTeacher.WorkingPeriods = newWorkingPeriodsEntities;
        existingTeacher.ScheduledWorkingPeriods = scheduledWorkingPeriods;
        */
    }
}