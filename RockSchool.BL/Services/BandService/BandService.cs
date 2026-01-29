using AutoMapper.Execution;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.BandService;

public class BandService(BandRepository bandRepository, BandStudentRepository bandStudentRepository, IScheduleService scheduleService)
    : IBandService
{
    public async Task<Band?> GetByIdAsync(Guid id)
    {
        var entity = await bandRepository.GetByIdAsync(id);
        return entity?.ToModel();
    }

    public async Task<Band[]> GetAllAsync()
    {
        var entities = await bandRepository.GetAllAsync();
        return entities.ToModel();
    }

    public async Task<Band[]> GetByTeacherIdAsync(Guid teacherId)
    {
        var entities = await bandRepository.GetByTeacherIdAsync(teacherId);
        return entities.ToModel();
    }

    public async Task<Guid> AddBandAsync(string name, Guid teacherId, BandMember[] members, Schedule[] schedules)
    {
        var band = new Band()
        {
            Name = name,
            TeacherId = teacherId,
            Status = 0, // DEV
        };
        var bandEntity = band.ToEntity();
        var bandId = await bandRepository.AddAsync(bandEntity);

        // BandsStudents - Save each member to database
        foreach (var member in members)
        {
            var newMember = new BandStudentEntity
            {
                BandStudentId = Guid.NewGuid(),
                BandId = bandId,
                StudentId = member.StudentId,
                BandRoleId = member.BandRoleId,
            };
            await bandStudentRepository.AddAsync(newMember);
        }

        // Schedules
        foreach (var schedule in schedules)
        {
            schedule.BandId = bandId;
            schedule.SubscriptionId = null; // Ensure only BandId is set
            await scheduleService.AddScheduleAsync(schedule);
        }

        return bandId;
    }

    public async Task UpdateBandAsync(Band band)
    {
        var entity = band.ToEntity();
        await bandRepository.UpdateAsync(entity);
    }

    public async Task DeleteBandAsync(Guid id)
    {
        await bandRepository.DeleteAsync(id);
    }
}