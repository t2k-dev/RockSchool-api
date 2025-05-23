﻿using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly DisciplineRepository _disciplineRepository;
    private readonly TeacherRepository _teacherRepository;
    private readonly ScheduledWorkingPeriodsRepository _scheduledWorkingPeriodsRepository;

    public TeacherService(TeacherRepository teacherRepository, DisciplineRepository disciplineRepository,
        ScheduledWorkingPeriodsRepository scheduledWorkingPeriodsRepository)
    {
        _teacherRepository = teacherRepository;
        _disciplineRepository = disciplineRepository;
        _scheduledWorkingPeriodsRepository = scheduledWorkingPeriodsRepository;
    }

    public async Task<Guid> AddTeacher(TeacherDto addTeacherDto)
    {
        var disciplineEntities = new List<DisciplineEntity>();
        if (addTeacherDto.DisciplineIds != null)
        {
            foreach (var disciplineId in addTeacherDto.DisciplineIds)
            {
                var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);
                if (discipline != null)
                {
                    disciplineEntities.Add(discipline);
                }
            }
        }

        var workingPeriodEntities = new List<WorkingPeriodEntity>();
        foreach (var workingPeriodDto in addTeacherDto.WorkingPeriods)
        {
            workingPeriodEntities.Add(new WorkingPeriodEntity
            {
                StartTime = workingPeriodDto.StartTime,
                EndTime = workingPeriodDto.EndTime,
                WeekDay = workingPeriodDto.WeekDay,
                RoomId = workingPeriodDto.RoomId,
            });
        }
        
        var teacher = new TeacherEntity
        {
            LastName = addTeacherDto.LastName,
            FirstName = addTeacherDto.FirstName,
            BirthDate = addTeacherDto.BirthDate,
            Phone = addTeacherDto.Phone,
            BranchId = addTeacherDto.BranchId.Value,
            Disciplines = disciplineEntities,
            WorkingPeriods = workingPeriodEntities,
            Sex = addTeacherDto.Sex,
            AllowGroupLessons = addTeacherDto.AllowGroupLessons,
            AgeLimit = addTeacherDto.AgeLimit
        };

        await _teacherRepository.AddAsync(teacher);

        var scheduledWorkingPeriodEntities = BuildScheduledWorkingPeriods(teacher.TeacherId, workingPeriodEntities);
        await _scheduledWorkingPeriodsRepository.AddRangeAsync(scheduledWorkingPeriodEntities);

        var savedTeacher = await _teacherRepository.GetByIdAsync(teacher.TeacherId);

        if (savedTeacher == null)
            throw new InvalidOperationException("Failed to add teacher.");

        return savedTeacher.TeacherId;
    }

    private List<ScheduledWorkingPeriodEntity> BuildScheduledWorkingPeriods(Guid teacherId, List<WorkingPeriodEntity> workingPeriodEntities)
    {
        var scheduledWorkingPeriodEntities = new List<ScheduledWorkingPeriodEntity>();
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddMonths(3);

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
                        WorkingPeriodId = workingPeriodEntity.WorkingPeriodId,
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
        var teacher = await _teacherRepository.GetByIdAsync(id);

        if (teacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

        var disciplineDtos = new List<DisciplineDto>();
        var disciplineIds = new List<int>();

        foreach (var teacherDiscipline in teacher.Disciplines)
        {
            disciplineDtos.Add(new DisciplineDto
            {
                Name = teacherDiscipline.Name,
                DisciplineId = teacherDiscipline.DisciplineId,
                IsActive = teacherDiscipline.IsActive,
                Teachers = teacherDiscipline.Teachers.ToDto(),
            });

            disciplineIds.Add(teacherDiscipline.DisciplineId);
        }
        
        var teacherDto = new TeacherDto
        {
            TeacherId = teacher.TeacherId,
            LastName = teacher.LastName,
            FirstName = teacher.FirstName,
            BirthDate = teacher.BirthDate,
            Sex = teacher.Sex,
            Phone = teacher.Phone,
            User = teacher.User,
            Disciplines = disciplineDtos,
            DisciplineIds = disciplineIds.ToArray(),
            AllowGroupLessons = teacher.AllowGroupLessons,
            AgeLimit = teacher.AgeLimit,
            BranchId = teacher.BranchId,
            WorkingPeriods = teacher.WorkingPeriods.ToDto(),
            ScheduledWorkingPeriods = teacher.ScheduledWorkingPeriods.ToDto(),
        };

        return teacherDto;
    }

    public async Task UpdateTeacherAsync(TeacherDto teacherDto)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(teacherDto.TeacherId);

        if (existingTeacher == null)
            throw new KeyNotFoundException(
                $"TeacherEntity with ID {teacherDto.TeacherId} was not found.");

        await UpdateTeacherDisciplines(teacherDto, existingTeacher);
        UpdateTeacherDetails(teacherDto, existingTeacher);

        await _teacherRepository.UpdateAsync(existingTeacher);
    }

    public async Task DeleteTeacherAsync(Guid id)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(id);

        if (existingTeacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

        await _teacherRepository.DeleteAsync(existingTeacher);
    }

    private async Task UpdateTeacherDisciplines(TeacherDto updateTeacherServiceRequestDto,
        TeacherEntity existingTeacherEntity)
    {
        existingTeacherEntity.Disciplines.Clear();
        var disciplines = new List<DisciplineEntity>();

        foreach (var disciplineId in updateTeacherServiceRequestDto.DisciplineIds)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);

            if (discipline != null)
                disciplines.Add(discipline);
        }

        existingTeacherEntity.Disciplines = disciplines;
    }

    private static void UpdateTeacherDetails(TeacherDto updateRequest,
        TeacherEntity? existingTeacher)
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

        // if (updateRequest.User != null)
        //     existingTeacher.UserId = updateRequest.User.UserId;

        // if (updateRequest.WorkingHoursEntity != null)
        //     existingTeacher.WorkingPeriods = updateRequest.WorkingHoursEntity;
    }

    public async Task<TeacherDto[]?>  GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge)
    {
        var teacherEntities = await _teacherRepository.GetTeachersAsync(branchId, disciplineId, studentAge);

        return teacherEntities.ToDto();
    }
}