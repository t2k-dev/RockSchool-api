﻿namespace RockSchool.Data.Entities;

public class DisciplineEntity
{
    public int Id { get; set; }
    public string DisciplineName { get; set; }
    public ICollection<TeacherEntity> Teachers { get; set; }
    public bool IsActive { get; set; }
}