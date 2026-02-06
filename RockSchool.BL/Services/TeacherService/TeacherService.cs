using RockSchool.BL.Services.ScheduledWorkingPeriodsService;
using RockSchool.BL.Teachers;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Disciplines;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IScheduledWorkingPeriodsService _scheduledWorkingPeriodsService;

    public TeacherService(ITeacherRepository teacherRepository, IDisciplineRepository disciplineRepository,
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
        var teacher = await _teacherRepository.GetByIdAsync(id);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Teacher with ID {id} was not found.");
        }

        /*var disciplines = teacherEntity.Disciplines.Select(teacherDiscipline => new Discipline
            {
                Name = teacherDiscipline.Name, 
                DisciplineId = teacherDiscipline.DisciplineId, 
                IsActive = teacherDiscipline.IsActive, 
                // DEV Teachers = teacherDiscipline.Teachers,
            })
            .ToList();*/

        return teacher;
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

    public async Task UpdateTeacherAsync(TeacherDto teacherDto, bool updateDisciplines, bool recalculatePeriods)
    {
        var teacher = await _teacherRepository.GetByIdAsync(teacherDto.TeacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {teacherDto.TeacherId} was not found.");
        }

        teacher.UpdateInfo(
            teacherDto.FirstName,
            teacherDto.FirstName,
            teacherDto.BirthDate,
            teacherDto.Sex,
            teacherDto.Phone,
            teacherDto.AgeLimit,
            teacherDto.AllowGroupLessons,
            teacherDto.AllowBands);

        // Disciplines
        if (updateDisciplines)
        {
            var disciplines = await _disciplineRepository.GetByIdsAsync(teacherDto.DisciplineIds);
            teacher.UpdateDisciplines(disciplines);
        }

        // Periods
        if (recalculatePeriods)
        {
            //UpdateTeacherWorkingPeriods(teacher, teacherDto.WorkingPeriods.ToList());
        }

        await _teacherRepository.UpdateAsync(teacher);
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