using System;

using RockSchool.Domain.Enums;

namespace RockSchool.WebApi.Models.Bands;

public class AddMemberToBandDto
{
    public Guid BandId { get; set; }
    public Guid StudentId { get; set; }
    public BandRoleId BandRoleId { get; set; }
}