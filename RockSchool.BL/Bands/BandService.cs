using AutoMapper.Execution;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;
using RockSchool.BL.Schedules;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Bands;

public class BandService(IBandRepository bandRepository, IBandMemberRepository bandMemberRepository, IScheduleService scheduleService, IUnitOfWork unitOfWork)
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

    public async Task<Guid> AddBandAsync(string name, Guid teacherId, BandMemberDto[] members, ScheduleSlotDto[] scheduleSlotDtos)
    {
        var band = Band.Create(name, teacherId);
        foreach (var member in members)
        {
            band.AddMember(member.StudentId, member.BandRoleId);
        }
        
        var scheduleId = await scheduleService.AddScheduleAsync(scheduleSlotDtos);
        band.AssignSchedule(scheduleId);

        var bandId = await bandRepository.AddAsync(band);

        await unitOfWork.SaveChangesAsync();

        return bandId;
    }

    public async Task AddBandMemberAsync(Guid bandId, Guid studentId, BandRoleId? bandRoleId)
    {
        var band = await bandRepository.GetByIdAsync(bandId);
        if (band == null)
            throw new InvalidOperationException($"Band with id {bandId} not found");

        var member = BandMember.Create(bandId, studentId, bandRoleId);
        await bandMemberRepository.AddAsync(member);

        //bandRepository.Update(band);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Band?> ActivateBandAsync(Guid id)
    {
        var band = await bandRepository.GetByIdAsync(id);
        if (band == null) return null;
        
        band.Activate();
        
        bandRepository.Update(band);

        await unitOfWork.SaveChangesAsync();

        return band;
    }

    public async Task<Band?> DeactivateBandAsync(Guid id)
    {
        var band = await bandRepository.GetByIdAsync(id);
        if (band == null) return null;
        
        band.Deactivate();
        
        bandRepository.Update(band);

        return band;
    }

    public async Task UpdateBandAsync(Band band)
    {
        bandRepository.Update(band);
    }

    public async Task DeleteBandAsync(Guid id)
    {
        await bandRepository.DeleteAsync(id);
    }




}