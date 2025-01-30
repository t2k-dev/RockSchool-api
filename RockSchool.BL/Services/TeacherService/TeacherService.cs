using AutoMapper;
using RockSchool.BL.Dtos.Service.Requests.TeacherService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        private readonly DisciplineRepository _disciplineRepository;
        protected readonly IMapper _mapper;

        public TeacherService(TeacherRepository teacherRepository, IMapper mapper,
            DisciplineRepository disciplineRepository)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _disciplineRepository = disciplineRepository;
        }

        public async Task AddTeacher(
            AddTeacherServiceRequestDto addTeacherDto)
        {
            var teacher = _mapper.Map<TeacherEntity>(addTeacherDto);

            await _teacherRepository.AddAsync(teacher);

            var savedTeacher = await _teacherRepository.GetByIdAsync(teacher.TeacherId);

            if (savedTeacher == null)
                throw new InvalidOperationException("Failed to add teacherEntity.");
        }

        public async Task<TeacherDto[]> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            var teacherDtos = _mapper.Map<TeacherDto[]>(teachers);

            return teacherDtos;
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
                throw new KeyNotFoundException($"TeacherEntity with ID {id} was not found.");

            var teacherDto = _mapper.Map<TeacherDto>(teacher);

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

        private static void UpdateTeacherDetails(UpdateTeacherServiceRequestDto updateRequest, TeacherEntity? existingTeacher)
        {
            if (existingTeacher == null)
                throw new KeyNotFoundException($"TeacherEntity with ID {updateRequest.TeacherId} was not found.");

            if (!string.IsNullOrWhiteSpace(updateRequest.FirstName))
                existingTeacher.FirstName = updateRequest.FirstName;

            if (!string.IsNullOrWhiteSpace(updateRequest.LastName))
                existingTeacher.LastName = updateRequest.LastName;

            if (!string.IsNullOrWhiteSpace(updateRequest.MiddleName))
                existingTeacher.MiddleName = updateRequest.MiddleName;

            if (updateRequest.BirthDate != default(DateTime))
                existingTeacher.BirthDate = updateRequest.BirthDate;

            if (updateRequest.Phone != default(long))
                existingTeacher.Phone = updateRequest.Phone;

            if (updateRequest.UserId.HasValue)
                existingTeacher.UserId = updateRequest.UserId.Value;

            if (updateRequest.WorkingHoursEntity != null)
                existingTeacher.WorkingHoursEntity = updateRequest.WorkingHoursEntity;
        }
    }
}