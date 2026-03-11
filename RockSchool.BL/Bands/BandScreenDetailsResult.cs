using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Bands;

public class BandScreenDetailsResult
{
    public Band Band { get; set; } = null!;
    public AttendanceWithAttendeesDto[] Attendances { get; set; } = [];
    public BandMember[] BandMembers { get; set; } = [];
}
