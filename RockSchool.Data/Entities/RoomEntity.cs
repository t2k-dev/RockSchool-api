﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class RoomEntity
{
    [Key]
    public int RoomId { get; set; }
    public int BranchId { get; set; }
    public BranchEntity Branch { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}