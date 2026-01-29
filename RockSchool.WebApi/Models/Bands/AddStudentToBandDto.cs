using System;

using RockSchool.Data.Enums;

namespace RockSchool.WebApi.Models.Bands;

public class AddStudentToBandDto
{
    public Guid BandId { get; set; }
    public Guid StudentId { get; set; }
    public BandRoleId BandRoleId { get; set; }
}