using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly DisciplineRepository _disciplineRepository;
    private readonly TeacherRepository _teacherRepository;
    private readonly BranchRepository _branchRepository;

    public TeacherService(TeacherRepository teacherRepository, DisciplineRepository disciplineRepository, BranchRepository branchRepository)
    {
        _teacherRepository = teacherRepository;
        _disciplineRepository = disciplineRepository;
        _branchRepository = branchRepository;
    }

    public async Task AddTeacher(TeacherDto addTeacherDto)
    {
        var teacher = new TeacherEntity
        {
            LastName = addTeacherDto.LastName,
            FirstName = addTeacherDto.FirstName,
            BirthDate = addTeacherDto.BirthDate,
            Phone = addTeacherDto.Phone,
            BranchId = addTeacherDto.BranchId.Value,
            //UserId = addTeacherDto.UserId
        };

        await _teacherRepository.AddAsync(teacher);

        var savedTeacher = await _teacherRepository.GetByIdAsync(teacher.TeacherId);

        if (savedTeacher == null)
            throw new InvalidOperationException("Failed to add teacher.");
    }

    public async Task<TeacherDto[]> GetAllTeachersAsync()
    {
        var teachers = await _teacherRepository.GetAllAsync();

        if (teachers == null || !teachers.Any())
            return Array.Empty<TeacherDto>();

        var teacherDtos = teachers.Select(t => new TeacherDto
        {
            TeacherId = t.TeacherId,
            LastName = t.LastName,
            FirstName = t.FirstName,
            BirthDate = t.BirthDate,
            Phone = t.Phone,
            User = t.User,
            Disciplines = t.Disciplines,
            // WorkingHoursEntity = t.WorkingPeriods
        }).ToArray();

        return teacherDtos;
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);

        if (teacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

        var teacherDto = new TeacherDto
        {
            TeacherId = teacher.TeacherId,
            LastName = teacher.LastName,
            FirstName = teacher.FirstName,
            BirthDate = teacher.BirthDate,
            Sex = teacher.Sex,
            Phone = teacher.Phone,
            User = teacher.User,
            Disciplines = teacher.Disciplines,
            AllowGroupLessons = teacher.AllowGroupLessons,
            AgeLimit = teacher.AgeLimit,
            BranchId = teacher.BranchId,
            // WorkingHoursEntity = teacher.WorkingPeriods
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

        foreach (var disciplineId in updateTeacherServiceRequestDto.Disciplines)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(disciplineId.DisciplineId);

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
}