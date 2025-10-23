﻿using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.BL.Services.ScheduledWorkingPeriodsService;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

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

    

    public async Task<TeacherDto[]> GetAllTeachersAsync()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        
        if (teachers == null || !teachers.Any())
            return Array.Empty<TeacherDto>();

        var teacherDtos = teachers.ToDto();

        // var teacherDtos = teachers.Select(t => new TeacherDto
        // {
        //     TeacherId = t.TeacherId,
        //     LastName = t.LastName,
        //     FirstName = t.FirstName,
        //     BirthDate = t.BirthDate,
        //     Phone = t.Phone,
        //     User = t.User,
        //     Disciplines = t.Disciplines?.Select(d => new DisciplineDto
        //     {
        //         DisciplineId = d.DisciplineId,
        //         Name = d.Name,
        //         IsActive = d.IsActive,
        //         Teachers = d.Teachers.ToDto(),
        //     }).ToArray(),
        //     WorkingPeriods = t.WorkingPeriods?.Select(w => new WorkingPeriodDto()
        //     {
        //         StartTime = w.StartTime,
        //         EndTime = w.EndTime,
        //         WeekDay = w.WeekDay,
        //         TeacherId = t.TeacherId,
        //         WorkingPeriodId = w.WorkingPeriodId,
        //     }).ToArray(),
        //     Branch = new BranchDto()
        //     {
        //         BranchId = t.BranchId,
        //         Name = t.Branch?.Name,
        //         Phone = t.Branch?.Phone,
        //         Address = t.Branch?.Address,
        //         Rooms = t.Branch?.Rooms?.ToDto(),
        //     }
        // }).ToArray();
        
        return teacherDtos;
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
    {
        var teacherEntity = await _teacherRepository.GetByIdAsync(id);
        if (teacherEntity == null)
        {
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");
        }

        var disciplines = teacherEntity.Disciplines.Select(teacherDiscipline => new DisciplineDto
            {
                Name = teacherDiscipline.Name, 
                DisciplineId = teacherDiscipline.DisciplineId, 
                IsActive = teacherDiscipline.IsActive, 
                Teachers = teacherDiscipline.Teachers.ToDto(),
            })
            .ToList();

        var teacher = new TeacherDto
        {
            TeacherId = teacherEntity.TeacherId,
            LastName = teacherEntity.LastName,
            FirstName = teacherEntity.FirstName,
            BirthDate = teacherEntity.BirthDate,
            Sex = teacherEntity.Sex,
            Phone = teacherEntity.Phone,
            User = teacherEntity.User,
            Disciplines = disciplines,
            DisciplineIds = disciplines.Select(d => d.DisciplineId).ToArray(),
            AllowGroupLessons = teacherEntity.AllowGroupLessons,
            AgeLimit = teacherEntity.AgeLimit,
            BranchId = teacherEntity.BranchId,
            WorkingPeriods = teacherEntity.WorkingPeriods.ToDto(),
            ScheduledWorkingPeriods = teacherEntity.ScheduledWorkingPeriods.ToDto(),
        };

        return teacher;
    }

    public async Task<TeacherDto[]?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
    {
        var teacherEntities = await _teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge);

        return teacherEntities.ToDto();
    }

    public async Task<Guid> AddTeacher(TeacherDto addTeacherDto)
    {
        var disciplines = await _disciplineRepository.GetByIdsAsync(addTeacherDto.DisciplineIds);
        var workingPeriodEntities = addTeacherDto.WorkingPeriods.ToEntities();
        var scheduledWorkingPeriods = BuildScheduledWorkingPeriods(workingPeriodEntities, addTeacherDto.TeacherId, DateTime.Now, 3);

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
            AgeLimit = addTeacherDto.AgeLimit,
            IsActive = addTeacherDto.IsActive,
        };

        await _teacherRepository.AddAsync(teacherEntity);

        return teacherEntity.TeacherId;
    }

    public async Task UpdateTeacherAsync(TeacherDto teacherDto, bool updateDisciplines, bool recalculatePeriods)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(teacherDto.TeacherId);
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

    private static void UpdateTeacherDetails(TeacherDto updateRequest, TeacherEntity existingTeacher)
    {
        if (existingTeacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {updateRequest.TeacherId} was not found.");

        if (!string.IsNullOrWhiteSpace(updateRequest.FirstName))
            existingTeacher.FirstName = updateRequest.FirstName;

        if (!string.IsNullOrWhiteSpace(updateRequest.LastName))
            existingTeacher.LastName = updateRequest.LastName;

        if (updateRequest.BirthDate != default)
            existingTeacher.BirthDate = updateRequest.BirthDate;

        if (updateRequest.Phone != default)
            existingTeacher.Phone = updateRequest.Phone;

        if (updateRequest.Sex != default)
            existingTeacher.Sex = updateRequest.Sex;

        if (updateRequest.AgeLimit != default)
            existingTeacher.AgeLimit = updateRequest.AgeLimit;

        if (updateRequest.AllowGroupLessons != default)
            existingTeacher.AllowGroupLessons = updateRequest.AllowGroupLessons;

        // if (updateRequest.User != null)
        //     existingTeacher.UserId = updateRequest.User.UserId;

        // if (updateRequest.WorkingHoursEntity != null)
        //     existingTeacher.WorkingPeriods = updateRequest.WorkingHoursEntity;
    }

    private List<ScheduledWorkingPeriodEntity> BuildScheduledWorkingPeriods(List<WorkingPeriodEntity> workingPeriodEntities, Guid teacherId, DateTime startDate, int months)
    {
        var scheduledWorkingPeriodEntities = new List<ScheduledWorkingPeriodEntity>();

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

                    scheduledWorkingPeriodEntities.Add(new ScheduledWorkingPeriodEntity
                    {
                        WorkingPeriod = workingPeriodEntity,
                        TeacherId = teacherId,
                        StartDate = periodStart,
                        EndDate = periodEnd,
                        RoomId = workingPeriodEntity.RoomId,
                    });
                }
            }
        }

        return scheduledWorkingPeriodEntities;
    }

    private void UpdateTeacherWorkingPeriods(TeacherEntity existingTeacher, List<WorkingPeriodDto> workingPeriods)
    {
        var newWorkingPeriodsEntities = workingPeriods.ToEntities();

        //existingTeacher.ScheduledWorkingPeriods.ToList().RemoveRange() (swp => swp.StartDate > DateTime.Now)
        // Exclude future scheduled periods that are not actual and add new ones.
        var scheduledWorkingPeriods = existingTeacher.ScheduledWorkingPeriods.Where(swp => swp.StartDate < DateTime.Now).ToList();

        var newScheduledWorkingPeriods = BuildScheduledWorkingPeriods(newWorkingPeriodsEntities, existingTeacher.TeacherId, DateTime.Now, 3);
        scheduledWorkingPeriods.AddRange(newScheduledWorkingPeriods);

        existingTeacher.WorkingPeriods.Clear();
        existingTeacher.WorkingPeriods = newWorkingPeriodsEntities;
        existingTeacher.ScheduledWorkingPeriods = scheduledWorkingPeriods;
    }
}