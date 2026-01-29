using System;

using RockSchool.Data.Enums;

namespace RockSchool.WebApi.Models.Bands;

public class BandStudentInfo
{
    public Guid BandStudentId { get; set; }
    public Guid BandId { get; set; }
    public Guid StudentId { get; set; }
    public object? Student { get; set; }
    public BandRoleId BandRoleId { get; set; }
}