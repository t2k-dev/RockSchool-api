using System;
using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models.Bands;

public class CreateBandRequest
{
    public string Name { get; set; }
    public BandMember[] Members { get; set; }
    public Guid TeacherId { get; set; }
    public Schedule[] Schedules { get; set; }
}