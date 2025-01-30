using RockSchool.BL.Dtos.Service.Requests.TeacherService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly DisciplineRepository _disciplineRepository;
    private readonly TeacherRepository _teacherRepository;

    public TeacherService(TeacherRepository teacherRepository,
        DisciplineRepository disciplineRepository)
    {
        _teacherRepository = teacherRepository;
        _disciplineRepository = disciplineRepository;
    }

    public async Task AddTeacher(AddTeacherServiceRequestDto addTeacherDto)
    {
        var teacher = new TeacherEntity
        {
            LastName = addTeacherDto.LastName,
            FirstName = addTeacherDto.FirstName,
            MiddleName = addTeacherDto.MiddleName,
            BirthDate = addTeacherDto.BirthDate,
            Phone = addTeacherDto.Phone,
            UserId = addTeacherDto.UserId
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
            MiddleName = t.MiddleName,
            BirthDate = t.BirthDate,
            Phone = t.Phone,
            UserId = t.UserId,
            UserEntity = t.UserEntity,
            Disciplines = t.Disciplines,
            WorkingHoursEntity = t.WorkingHoursEntity
        }).ToArray();

        return teacherDtos;
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(int id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);

        if (teacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

        var teacherDto = new TeacherDto
        {
            TeacherId = teacher.TeacherId,
            LastName = teacher.LastName,
            FirstName = teacher.FirstName,
            MiddleName = teacher.MiddleName,
            BirthDate = teacher.BirthDate,
            Phone = teacher.Phone,
            UserId = teacher.UserId,
            UserEntity = teacher.UserEntity,
            Disciplines = teacher.Disciplines,
            WorkingHoursEntity = teacher.WorkingHoursEntity
        };

        return teacherDto;
    }

    public async Task UpdateTeacherAsync(UpdateTeacherServiceRequestDto updateTeacherServiceRequestDto)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(updateTeacherServiceRequestDto.TeacherId);

        if (existingTeacher == null)
            throw new KeyNotFoundException(
                $"TeacherEntity with ID {updateTeacherServiceRequestDto.TeacherId} was not found.");

        await UpdateTeacherDisciplines(updateTeacherServiceRequestDto, existingTeacher);
        UpdateTeacherDetails(updateTeacherServiceRequestDto, existingTeacher);

        await _teacherRepository.UpdateAsync(existingTeacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(id);

        if (existingTeacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

        await _teacherRepository.DeleteAsync(existingTeacher);
    }

    private async Task UpdateTeacherDisciplines(UpdateTeacherServiceRequestDto updateTeacherServiceRequestDto,
        TeacherEntity existingTeacherEntity)
    {
        existingTeacherEntity.Disciplines.Clear();
        var disciplines = new List<DisciplineEntity>();

        foreach (var disciplineId in updateTeacherServiceRequestDto.Disciplines)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);

            if (discipline != null)
                disciplines.Add(discipline);
        }

        existingTeacherEntity.Disciplines = disciplines;
    }

    private static void UpdateTeacherDetails(UpdateTeacherServiceRequestDto updateRequest,
        TeacherEntity? existingTeacher)
    {
        if (existingTeacher == null)
            throw new KeyNotFoundException($"TeacherEntity with ID {updateRequest.TeacherId} was not found.");

        if (!string.IsNullOrWhiteSpace(updateRequest.FirstName))
            existingTeacher.FirstName = updateRequest.FirstName;

        if (!string.IsNullOrWhiteSpace(updateRequest.LastName))
            existingTeacher.LastName = updateRequest.LastName;

        if (!string.IsNullOrWhiteSpace(updateRequest.MiddleName))
            existingTeacher.MiddleName = updateRequest.MiddleName;

        if (updateRequest.BirthDate != default)
            existingTeacher.BirthDate = updateRequest.BirthDate;

        if (updateRequest.Phone != default)
            existingTeacher.Phone = updateRequest.Phone;

        if (updateRequest.UserId.HasValue)
            existingTeacher.UserId = updateRequest.UserId.Value;

        if (updateRequest.WorkingHoursEntity != null)
            existingTeacher.WorkingHoursEntity = updateRequest.WorkingHoursEntity;
    }
}