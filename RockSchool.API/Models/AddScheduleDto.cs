﻿using System;

namespace RockSchool.API.Models
{
    public class AddScheduleDto
    {
        public int StudentId { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public int Duration { get; set; }
        public int DisciplineId { get; set; }
    }
}
