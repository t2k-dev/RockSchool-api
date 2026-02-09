using RockSchool.BL.Services.ScheduledWorkingPeriodsService;
using RockSchool.BL.Teachers;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService(
    ITeacherRepository teacherRepository,
    IDisciplineRepository disciplineRepository,
    IScheduledWorkingPeriodsRepository scheduledWorkingPeriodsRepository,
    IWorkingPeriodsRepository workingPeriodsRepository,
    IUnitOfWork unitOfWork)
    : ITeacherService
{
    public async Task<Teacher[]> GetAllTeachersAsync()
    {
        var teacherEntities = await teacherRepository.GetAllAsync();
        if (!teacherEntities.Any())
        {
            return [];
        }

        return teacherEntities;
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid id)
    {
        var teacher = await teacherRepository.GetByIdAsync(id);
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
        return await teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge);
    }

    public async Task<Teacher[]?> GetRehearsableTeachersAsync(int branchId)
    {
        return await teacherRepository.GetRehearsableTeachersAsync(branchId);
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

    public async Task UpdateTeacherAsync(TeacherDto teacherDto, bool updateDisciplines)
    {
        var teacher = await teacherRepository.GetByIdAsync(teacherDto.TeacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Teacher with ID {teacherDto.TeacherId} was not found.");
        }

        teacher.UpdateInfo(
            teacherDto.FirstName,
            teacherDto.LastName,
            teacherDto.BirthDate,
            teacherDto.Sex,
            teacherDto.Phone,
            teacherDto.AgeLimit,
            teacherDto.AllowGroupLessons,
            teacherDto.AllowBands);

        // Disciplines
        if (updateDisciplines)
        {
            var disciplines = await disciplineRepository.GetByIdsAsync(teacherDto.DisciplineIds);
            teacher.UpdateDisciplines(disciplines);
        }

        teacherRepository.Update(teacher);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePeriodsAsync(Guid teacherId, WorkingPeriodDto[] workingPeriodDtos, DateTime recalculatePeriodsAfter)
    {
        var teacher = await teacherRepository.GetByIdAsync(teacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Teacher with ID {teacherId} was not found.");
        }

        // Delete existing periods via repository
        //await workingPeriodsRepository.DeleteWorkingPeriodsByTeacherId(teacherId);

        // Create and add new periods via repository
        var workingPeriods = new List<WorkingPeriod>();
        foreach (var workingPeriodDto in workingPeriodDtos)
        {
            var period = WorkingPeriod.Create(teacherId,
                workingPeriodDto.WeekDay, 
                workingPeriodDto.StartTime,
                workingPeriodDto.EndTime, 
                workingPeriodDto.RoomId
                );

            workingPeriods.Add(period);
        }

        await workingPeriodsRepository.DeleteWorkingPeriodsByTeacherId(teacherId);
        await workingPeriodsRepository.AddRangeAsync(workingPeriods.ToArray());

        scheduledWorkingPeriodsRepository.DeleteForTeacherAfter(teacherId, recalculatePeriodsAfter);
        var newScheduledWorkingPeriods = BuildScheduledWorkingPeriods(workingPeriods.ToArray(), teacher.TeacherId, recalculatePeriodsAfter, 12);
        await scheduledWorkingPeriodsRepository.AddRangeAsync(newScheduledWorkingPeriods);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task SetTeacherActiveAsync(Guid id, bool isActive)
    {
        var existingTeacher = await teacherRepository.GetByIdAsync(id);
        if (existingTeacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        existingTeacher.SetActiveStatus(isActive);

        teacherRepository.Update(existingTeacher);
    }

    public async Task DeleteTeacherAsync(Guid id)
    {
        var existingTeacher = await teacherRepository.GetByIdAsync(id);
        if (existingTeacher == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        await teacherRepository.DeleteAsync(existingTeacher);
    }

    private List<ScheduledWorkingPeriod> BuildScheduledWorkingPeriods(WorkingPeriod[] workingPeriods, Guid teacherId, DateTime startDate, int months)
    {
        var scheduledWorkingPeriods = new List<ScheduledWorkingPeriod>();

        startDate = startDate.Date;
        var endDate = startDate.AddMonths(months);

        for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
        {
            foreach (var workingPeriodEntity in workingPeriods)
            {
                if ((int)currentDate.DayOfWeek == workingPeriodEntity.WeekDay)
                {
                    var localStart = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.StartTime), DateTimeKind.Local);
                    var localEnd = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.EndTime), DateTimeKind.Local);

                    var periodStart = localStart.ToUniversalTime();
                    var periodEnd = localEnd.ToUniversalTime();

                    var scheduledWorkingPeriod = ScheduledWorkingPeriod.Create(teacherId, periodStart, periodEnd, workingPeriodEntity.RoomId);
                    scheduledWorkingPeriods.Add(scheduledWorkingPeriod);
                }
            }
        }

        return scheduledWorkingPeriods;
    }
}