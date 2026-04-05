using AutoMapper.Execution;
using RockSchool.BL.Attendances;
using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;
using RockSchool.BL.Schedules;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Bands;

public class BandService(
    IBandRepository bandRepository,
    IBandMemberRepository bandMemberRepository,
    IScheduleService scheduleService,
    IAttendanceRepository attendanceRepository,
    IUnitOfWork unitOfWork)
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

    public async Task<Band[]> GetActiveByBranchIdAsync(int branchId)
    {
        return await bandRepository.GetActiveByBranchIdAsync(branchId);
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

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Band?> ActivateBandAsync(Guid bandId)
    {
        var band = await bandRepository.GetByIdAsync(bandId);
        if (band == null) return null;
        
        band.Activate();

        await unitOfWork.SaveChangesAsync();

        return band;
    }

    public async Task<Band?> DeactivateBandAsync(Guid bandId)
    {
        var band = await bandRepository.GetByIdAsync(bandId);
        if (band == null) return null;
        
        band.Deactivate();

        await unitOfWork.SaveChangesAsync();

        return band;
    }

    public async Task CreateAttendances(Guid bandId, DateTime startDate)
    {
        var band = await bandRepository.GetByIdWithScheduleAsync(bandId);
        if (band == null)
            throw new InvalidOperationException($"Band with id {bandId} not found");

        if (!band.BranchId.HasValue)
            throw new InvalidOperationException("Band branch is not assigned");

        if (band.Schedule == null)
            throw new InvalidOperationException("Band schedule is not assigned");

        var attendances = AttendanceScheduleHelper.Generate(
            10, 
            120,
            DateOnly.FromDateTime(startDate),
            band.BranchId.Value,
            null,
            band.TeacherId,
            band.Schedule.ScheduleSlots.ToDto(),
            AttendanceType.BandRehearsal,
            bandId
            );

        foreach (var attendance in attendances)
        {
            await attendanceRepository.AddAsync(attendance);
        }

        await unitOfWork.SaveChangesAsync();
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
