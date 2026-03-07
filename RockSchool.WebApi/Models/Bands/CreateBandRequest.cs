using System;
using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Bands;

public class CreateBandRequest
{
    public string Name { get; set; }
    public BandMemberDto[] Members { get; set; }
    public Guid TeacherId { get; set; }
    public ScheduleSlotInfo[] ScheduleSlots { get; set; }
}