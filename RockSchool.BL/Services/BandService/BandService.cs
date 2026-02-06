using AutoMapper.Execution;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models.Dtos;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Bands;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.BandService;

public class BandService(IBandRepository bandRepository, IBandStudentRepository bandStudentRepository, IScheduleService scheduleService)
    : IBandService
{
    public async Task<Band?> GetByIdAsync(Guid id)
    {
        var entity = await bandRepository.GetByIdAsync(id);
        return entity;
    }

    public async Task<Band[]> GetAllAsync()
    {
        return await bandRepository.GetAllAsync();
    }

    public async Task<Band[]> GetByTeacherIdAsync(Guid teacherId)
    {
        return await bandRepository.GetByTeacherIdAsync(teacherId);
    }

    public async Task<Guid> AddBandAsync(string name, Guid teacherId, BandMember[] members, Schedule[] schedules)
    {
        throw new NotImplementedException();
        /*

        var band = Band.Create(name, teacherId);
        var bandId = await bandRepository.AddAsync(band);

        // BandsStudents - Save each member to database
        foreach (var member in members)
        {
            var newMember = new BandStudent
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

        return bandId;*/
    }

    public async Task UpdateBandAsync(Band band)
    {
        await bandRepository.UpdateAsync(band);
    }

    public async Task DeleteBandAsync(Guid id)
    {
        await bandRepository.DeleteAsync(id);
    }
}